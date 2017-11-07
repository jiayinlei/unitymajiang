using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PdkPageEvent : EventManager {

    private CharacterType firstPlayer;
    private CharacterType currentAuthority;//当前出牌者
    private CharacterType biggest;//最大出牌者
    public List<Card> seclectCards;

    public event CardEvent smartCard;//电脑智能出牌
    public event CardEvent activeButton;//激活按钮
    public override void InformationSetting()
    {
        throw new NotImplementedException();

    }
    public CharacterType Biggest
    {
        set { biggest = value; }
        get { return biggest; }
    }

    void OutCardBtnClick()
    {
        CheckSelectCards();
    }
    void MyCardClick()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        string str = obj.name;
        int dex = int.Parse(str.Split ('_').ToList()[1]);
        
        Debug.Log (str);
        //if (obj.GetComponent<CardSprite>().Select)
        //{
        //    ChangeMyHandCardPosition(dex);
        //    obj.GetComponent<CardSprite>().Select = false;
        //}
        //else
        //{
        //    ChangeMyHandCardPosition(dex);
        //    obj.GetComponent<CardSprite>().Select = true;
        //}
        

    }

    IEnumerator SetMyAllhandCardTrue()
    {
        for (int i = 3; i < 19; i++)
        {
            yield return new WaitForSeconds(0.05f);
            SetActive(this .BindingSource [i],true);
        }
        
    }
    void ChangeMyHandCardPosition(int dex,bool select)
    {
        GameObject obj = this.BindingSource[dex + 2];
        obj.transform.position = new Vector3 (obj.transform.position.x , obj.transform.position.y +30f, obj.transform.position.z );
    }
    /// <summary>
    /// 洗牌+发牌
    /// </summary>
    public void DealCards()
    {
        //洗牌
        Deck.Instance.Shuffle();
        //return;
        //发牌
        CharacterType currentCharacter = CharacterType.Player;
        for (int i = 0; i < 48; i++)
        {
            if (currentCharacter == CharacterType.Desk)
            {
                currentCharacter = CharacterType.Player;
            }
            HandCards cards = this .BindingSource [(int)currentCharacter - 1].GetComponent<HandCards>();
            Card movedCard = Deck.Instance.Deal();
            if (movedCard.GetCardWeight == CardWeight.Three && movedCard.GetCardSuit == CardSuits.Spade)
            {
                firstPlayer = currentCharacter;
                Debug.Log("首发玩家" + firstPlayer.ToString());
            }
            cards.AddCard(movedCard);
            currentCharacter++;
        }
        for (int i = 0; i < 3; i++)
        {
            HandCards cards = this.BindingSource[i].GetComponent<HandCards>();
            cards.Sort();
        }
        for (int i = 3; i < 19; i++)
        {
            this.BindingSource[i].AddComponent<CardSprite>().Poker = this.BindingSource[0].GetComponent<HandCards>()[i - 3];
        }

        this.Init(firstPlayer);

    }
    /// <summary>
    /// 初始化开场顺序
    /// </summary>
    /// <param name="type"></param>
    public void Init(CharacterType type)
    {
        currentAuthority = type;
        Biggest = type;
        if (currentAuthority == CharacterType.Player)
        {
            //初始为玩家 ， 玩家必须出牌
            //activeButton(false, true);
            SetActive(this .BindingSource [19],true); 
        }
        else
        {
            //电脑自动出牌
            AutoDiscardCard(true,false);
            SetActive(this.BindingSource[19], false);
        }
    }

    public bool CheckSelectCards()
    {
        CardSprite[] sprites = this.BindingSource[21].GetComponentsInChildren<CardSprite>();
        //找出所有选中的牌
        List<Card> selectedCardsList = new List<Card>();
        List<CardSprite> selectedSpriteList = new List<CardSprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].Select)
            {
                selectedSpriteList.Add(sprites[i]);
                selectedCardsList.Add(sprites[i].Poker);
            }
        }
        //排好序
        selectedCardsList = CardRules.SortCards(selectedCardsList, true);
        //出牌
        return CheckPlayCards(selectedCardsList, selectedSpriteList);
    }
    /// <summary>
    /// 检测玩家出牌
    /// </summary>
    /// <param name="selectedCardsList"></param>
    /// <param name="selectedSpriteList"></param>
    bool CheckPlayCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
    {
        //GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
        Card[] selectedCardsArray = selectedCardsList.ToArray();
        //检测是否符合出牌规则
        CardsType type;
        if (CardRules.PopEnable(selectedCardsArray, out type))
        {

            CardsType rule = DeskCardsCache.Instance.Rule;
            if (OrderController.Instance.Biggest == OrderController.Instance.Type)
            {
                PlayCards(selectedCardsList, type, selectedSpriteList);
                return true;
            }
            else if (DeskCardsCache.Instance.Rule == CardsType.None)
            {
                PlayCards(selectedCardsList, type, selectedSpriteList);
                return true;
            }
            //炸弹
            else if (type == CardsType.Boom && rule != CardsType.Boom)
            {
                //controller.Multiples = 2;
                PlayCards(selectedCardsList, type, selectedSpriteList);
                return true;
            }
            else if (type == CardsType.Boom && rule == CardsType.Boom &&
               GameController.GetWeight(selectedCardsArray, type) > DeskCardsCache.Instance.TotalWeight)
            {
                //controller.Multiples = 2;
                PlayCards(selectedCardsList, type, selectedSpriteList);
                return true;
            }
            else if ((type == rule) && (selectedCardsArray.Length == DeskCardsCache.Instance.CardsCount) && (GameController.GetWeight(selectedCardsArray, type) > DeskCardsCache.Instance.TotalWeight))
            {
                PlayCards(selectedCardsList, type, selectedSpriteList);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 玩家出牌
    /// </summary>
    /// <param name="selectedCardsList"></param>
    /// <param name="selectedSpriteList"></param>
    void PlayCards(List<Card> selectedCardsList, CardsType type,List<CardSprite> selectedSpriteList = null )
    {
        HandCards player = this.BindingSource[(int)currentAuthority - 1].GetComponent<HandCards>();
        DeskCardsCache.Instance.Clear();
        DeskCardsCache.Instance.Rule = type;

        SetActive(BindingSource[19], false);
        if (selectedSpriteList != null && currentAuthority == CharacterType.Player)
        {
            for (int i = 0; i < selectedSpriteList.Count; i++)
            {
                //先进行卡牌移动
                player.PopCard(selectedSpriteList[i].Poker);
                DeskCardsCache.Instance.AddCard(selectedSpriteList[i].Poker);
                selectedSpriteList[i].GoToPosition(this.BindingSource[20], i);
            }
        }
        else if(selectedSpriteList == null && currentAuthority == CharacterType.PlayerOne)
        {

        }

        this.PlayAudioClip(type , selectedCardsList);
        this.BindingSource[21].GetComponent<GridLayoutGroup>().enabled = true;
        DeskCardsCache.Instance.Sort();

        //GameController.UpdateLeftCardsCount(CharacterType.Player, player.CardsCount);

        if (player.CardsCount == 0)
        {
            //GameObject.Find("GameController").GetComponent<GameController>().GameOver();
            Debug.Log("66666666666666666666666666666666666GameOver");
        }      
        else
        {
            Biggest = CharacterType.Player;
            Turn();
        }
    }

    /// <summary>
    /// 出牌轮转
    /// </summary>
    public void Turn()
    {
        currentAuthority += 1;

        if (currentAuthority == CharacterType.Desk)
        {
            currentAuthority = CharacterType.Player;
        }

        if (currentAuthority == CharacterType.PlayerOne ||
            currentAuthority == CharacterType.PlayerTwo)
        {
            AutoDiscardCard(biggest == currentAuthority, false);
        }
        else if (currentAuthority == CharacterType.Player)
        {
            //activeButton(biggest != currentAuthority);
            if (biggest != currentAuthority)
            {
                AutoDiscardCard(biggest == currentAuthority, true);
            }
            else
            {
                SetActive(this.BindingSource[19], true);
            }


        }

    }
     void AutoDiscardCard(bool isNone, bool isMyTurn)
    {
        HandCards cards = this.BindingSource[(int)currentAuthority - 1].GetComponent<HandCards>();
        if (currentAuthority == cards.cType)
        {
            StartCoroutine(DelayDiscardCard(isNone, isMyTurn));

        }

    }
    /// <summary>
    /// 延时出牌
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator DelayDiscardCard(bool isNone, bool isMyTurn)
    {
        yield return new WaitForSeconds(3.0f);
        CardsType rule = isNone ? CardsType.None : DeskCardsCache.Instance.Rule;
        int deskWeight = DeskCardsCache.Instance.TotalWeight;
        //根据桌面牌的类型和权值大小出牌
        switch (rule)
        {
            case CardsType.None:
                List<Card> discardCards_00 = FirstCard();
                if (discardCards_00.Count != 0)
                {
                    RemoveCards(discardCards_00);
                    DiscardCards(discardCards_00,null);
                }
                break;
            case CardsType.Double:
                List<Card> conformCards_01 = FindDouble(GetAllCards(), deskWeight, false);
                if (conformCards_01.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(conformCards_01);
                    DiscardCards(conformCards_01, null);
                }
                else if ((conformCards_01.Count != 0 && isMyTurn == true))
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true ,false );
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else if (conformCards_01.Count == 0 && isMyTurn == true)
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true ,false );
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                else
                {
                    OrderController.Instance.Turn();
                    ShowNotice();
                }
                break;
            case CardsType.Single:
                List<Card> conformCards_02 = FindSingle(GetAllCards(), deskWeight, false);
                if (conformCards_02.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(conformCards_02);
                    DiscardCards(conformCards_02, null);
                }
                else if (conformCards_02.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource [19],true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else if (conformCards_02.Count == 0 && isMyTurn == true)
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                    }
                    else
                    {
                        Turn();
                        ShowNotice();
                    }
                }
                else
                {
                    Turn();
                    ShowNotice();
                }
                break;
            case CardsType.OnlyThree:
                List<Card> conformCards_03 = FindOnlyThree(GetAllCards(), deskWeight, false);
                if (conformCards_03.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(conformCards_03);
                    DiscardCards(conformCards_03, null);
                }
                else if (conformCards_03.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else if (conformCards_03.Count == 0 && isMyTurn == true)
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                else
                {
                    OrderController.Instance.Turn();
                    ShowNotice();
                }
                break;
            case CardsType.Straight:
                List<Card> conformCards_04 = FindStraight(GetAllCards(), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount, false);
                if (conformCards_04.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(conformCards_04);
                    DiscardCards(conformCards_04, null);
                }
                else if (conformCards_04.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, GetSprite(boom));
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //      interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.ThreeAndOne:
                List<Card> discardCards_06 = FindThreeAndOne(GetAllCards(), deskWeight, false);
                if (discardCards_06.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(discardCards_06);
                    DiscardCards(discardCards_06, null);
                }
                else if (discardCards_06.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, null);
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.ThreeAndTwo:
                List<Card> discardCards_08 = FindThreeAndTwo(GetAllCards(), deskWeight, false);
                if (discardCards_08.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(discardCards_08);
                    DiscardCards(discardCards_08, null);
                }
                else if (discardCards_08.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, null);
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.DoubleStraight:
                List<Card> conformCards09 = FindDoubleStraight(GetAllCards(), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount);
                if (conformCards09.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(conformCards09);
                    DiscardCards(conformCards09, null);
                }
                else if (conformCards09.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, null);
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.TripleStraight:
                List<Card> boom_01 = FindTripleStraight(GetAllCards(), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount);
                if (boom_01.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(boom_01);
                    DiscardCards(boom_01, null);
                }
                else if (boom_01.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, null);
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.Boom:
                List<Card> boom_0 = FindBoom(GetAllCards(), deskWeight, true);
                if (boom_0.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(boom_0);
                    DiscardCards(boom_0, null);
                }
                else if (boom_0.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    OrderController.Instance.Turn();
                    ShowNotice();
                }
                break;
            case CardsType.FourAndThree:
                List<Card> boom_09 = FindFourAndThree(GetAllCards(), deskWeight, true);
                if (boom_09.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(boom_09);
                    DiscardCards(boom_09, null);
                }
                else if (boom_09.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, null);
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.TripleStraightAndWing:
                List<Card> boom_10 = FindTripleStraightAndWing(GetAllCards(), DeskCardsCache.Instance.FlyWingMinWeight, DeskCardsCache.Instance.CardsCount);
                if (boom_10.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(boom_10);
                    DiscardCards(boom_10, null);
                }
                else if (boom_10.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                    SetActive(BindingSource[19], true);
                    BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                }
                else
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0 && isMyTurn != true)
                    {
                        RemoveCards(boom);
                        DiscardCards(boom, null);
                    }
                    else if (boom.Count != 0 && isMyTurn == true)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
                        SetActive(BindingSource[19], true);
                        BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 出牌动画
    /// </summary>
    /// <param name="selectedCardsList"></param>
    /// <param name="selectedSpriteList"></param>
    protected void DiscardCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
    {
        Card[] selectedCardsArray = selectedCardsList.ToArray();
        //检测是否符合出牌规则
        CardsType type;
        if (CardRules.PopEnable(selectedCardsArray, out type))
        {

            HandCards player = this.BindingSource[(int)currentAuthority - 1].GetComponent<HandCards>();
            //如果符合将牌从手牌移到出牌缓存区
            DeskCardsCache.Instance.Clear();
            DeskCardsCache.Instance.Rule = type;

            for (int i = 0; i < selectedCardsList.Count; i++)
            {
                DeskCardsCache.Instance.AddCard(selectedCardsList[i]);
                if (currentAuthority == CharacterType.PlayerOne)
                {
                    this.BindingSource[39 + i].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format ("pdkSprite/Cards/{0}", selectedCardsList[i].GetCardIndex));
                    SetActive(this.BindingSource[39 + i],true);

                }
                else if(currentAuthority == CharacterType.PlayerTwo)
                {
                    this.BindingSource[23 + i].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/Cards/{0}", selectedCardsList[i].GetCardIndex));
                    SetActive(this.BindingSource[23 + i], true);
                }
                //selectedSpriteList[i].transform.parent = GameObject.Find("Desk").transform;
                //selectedSpriteList[i].Poker = selectedSpriteList[i].Poker;
            }
            PlayAudioClip(type, selectedCardsList);
            DeskCardsCache.Instance.Sort();
            //GameController.AdjustCardSpritsPosition(CharacterType.Desk);
            //GameController.AdjustCardSpritsPosition(player.cType);

            //GameController.UpdateLeftCardsCount(player.cType, player.CardsCount);

            if (player.CardsCount == 0)
            {
                //GameObject.Find("GameController").GetComponent<GameController>().GameOver();
                Debug.Log("gameover666666666666666666666666666");
            }
            else
            {
                Biggest = player.cType;
                Turn();
            }
        }
    }

    /// <summary>
    /// 获取所有手牌
    /// </summary>
    /// <returns></returns>
    protected List<Card> GetAllCards(List<Card> exclude = null)
    {
        List<Card> cards = new List<Card>();
        HandCards allCards = this.BindingSource[(int)currentAuthority - 1].GetComponent<HandCards>();
        bool isContinue = false;
        for (int i = 0; i < allCards.CardsCount; i++)
        {
            isContinue = false;
            if (exclude != null)
            {
                for (int j = 0; j < exclude.Count; j++)
                {
                    if (allCards[i] == exclude[j])
                    {
                        isContinue = true;
                        break;
                    }

                }
            }

            if (!isContinue)
                cards.Add(allCards[i]);
        }
        //从小到大排序
        cards = CardRules.SortCards(cards, true);
        return cards;
    }

    /// <summary>
    /// 获得card对应的精灵
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    protected List<CardSprite> GetSprite(List<Card> cards)
    {
        HandCards t = this.BindingSource[(int)currentAuthority - 1].GetComponent<HandCards>();
        CardSprite[] sprites = GameObject.Find(t.cType.ToString()).GetComponentsInChildren<CardSprite>();

        List<CardSprite> selectedSpriteList = new List<CardSprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[j] == sprites[i].Poker)
                {
                    selectedSpriteList.Add(sprites[i]);
                    break;
                }
            }
        }

        return selectedSpriteList;
    }

    /// <summary>
    /// 移除手牌
    /// </summary>
    /// <param name="cards"></param>
    protected void RemoveCards(List<Card> cards)
    {
        HandCards allCards = this.BindingSource[(int)currentAuthority - 1].GetComponent<HandCards>();
        for (int j = 0; j < cards.Count; j++)
        {
            for (int i = 0; i < allCards.CardsCount; i++)
            {
                if (cards[j] == allCards[i])
                {
                    allCards.PopCard(cards[j]);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 找到手牌中符合要求的炸弹
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindBoom(List<Card> allCards, int weight, bool equal)
    {
        List<Card> ret = new List<Card>();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (i <= allCards.Count - 4)
            {
                //先找普通炸弹
                if (allCards[i].GetCardWeight == allCards[i + 1].GetCardWeight &&
                    allCards[i].GetCardWeight == allCards[i + 2].GetCardWeight &&
                    allCards[i].GetCardWeight == allCards[i + 3].GetCardWeight)
                {
                    int totalWeight = (int)allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight + (int)allCards[i + 2].GetCardWeight
                        + (int)allCards[i + 4].GetCardWeight;
                    if (equal)
                    {
                        if (totalWeight >= weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i + 1]);
                            ret.Add(allCards[i + 2]);
                            ret.Add(allCards[i + 3]);
                            break;
                        }
                    }
                    else
                    {
                        if (totalWeight > weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i + 1]);
                            ret.Add(allCards[i + 2]);
                            ret.Add(allCards[i + 3]);
                            break;
                        }
                    }

                }
            }
        }
        return ret;
    }

    /// <summary>
    /// 找到手牌中符合要求的是对子
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindDouble(List<Card> allCards, int weight, bool equal)
    {
        List<Card> ret = new List<Card>();
        for (int i = allCards.Count -1 ; i >=0; i--)
        {
            if (i > 1)
            {
                if (allCards[i].GetCardWeight == allCards[i - 1].GetCardWeight)
                {
                    int totalWeight = (int)allCards[i].GetCardWeight + (int)allCards[i - 1].GetCardWeight;
                    if (equal)
                    {
                        if (totalWeight >= weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i - 1]);
                            break;
                        }
                    }
                    else
                    {
                        if (totalWeight > weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i - 1]);
                            break;
                        }
                    }

                }
            }
        }

        return ret;
    }

    /// <summary>
    /// 找到手牌中符合要求的是单牌
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindSingle(List<Card> allCards, int weight, bool equal)
    {
        List<Card> ret = new List<Card>();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (equal)
            {
                if ((int)allCards[(allCards.Count -i -1)].GetCardWeight > weight)
                {
                    ret.Add(allCards[(allCards.Count - i - 1)]);
                    break;
                }
            }
            else
            {
                if ((int)allCards[(allCards.Count - i - 1)].GetCardWeight > weight)
                {
                    ret.Add(allCards[(allCards.Count - i - 1)]);
                    break;
                }
            }

        }
        return ret;
    }

    /// <summary>
    /// 找到手牌中符合要求的是3章
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindOnlyThree(List<Card> allCards, int weight, bool equal)
    {
        List<Card> ret = new List<Card>();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (i <= allCards.Count - 3)
            {
                if (allCards[i].GetCardWeight == allCards[i + 1].GetCardWeight &&
                    allCards[i].GetCardWeight == allCards[i + 2].GetCardWeight)
                {
                    int totalWeight = (int)allCards[i].GetCardWeight +
                        (int)allCards[i + 1].GetCardWeight +
                        (int)allCards[i + 2].GetCardWeight;

                    if (equal)
                    {
                        if (totalWeight >= weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i + 1]);
                            ret.Add(allCards[i + 2]);
                            break;
                        }
                    }
                    else
                    {
                        if (totalWeight > weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i + 1]);
                            ret.Add(allCards[i + 2]);
                            break;
                        }
                    }

                }
            }
        }

        return ret;
    }

    /// <summary>
    /// 找到手牌中符合要求的是连子
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindStraight(List<Card> allCards, int minWeight, int length, bool equal)
    {
        List<Card> ret = new List<Card>();
        int counter = 1;
        List<int> indeies = new List<int>();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (i <= allCards.Count - 5)
            {
                int weight = (int)allCards[i].GetCardWeight;
                if (equal)
                {
                    if (weight >= minWeight)
                    {
                        counter = 1;
                        indeies.Clear();

                        for (int j = i + 1; j < allCards.Count; j++)
                        {
                            if (allCards[j].GetCardWeight > CardWeight.One)
                                break;

                            if ((int)allCards[j].GetCardWeight - weight == counter)
                            {
                                counter++;
                                indeies.Add(j);
                            }

                            if (counter == length)
                                break;
                        }
                    }
                }
                else
                {
                    if (weight > minWeight)
                    {
                        counter = 1;
                        indeies.Clear();

                        for (int j = i + 1; j < allCards.Count; j++)
                        {
                            if (allCards[j].GetCardWeight > CardWeight.One)
                                break;
                            if ((int)allCards[j].GetCardWeight - weight == counter)
                            {
                                counter++;
                                indeies.Add(j);
                            }

                            if (counter == length)
                                break;
                        }
                    }
                }

            }
            if (counter == length)
            {
                indeies.Insert(0, i);
                break;
            }

        }

        if (counter == length)
        {
            for (int i = 0; i < indeies.Count; i++)
            {
                ret.Add(allCards[indeies[i]]);
            }
        }

        return ret;
    }

    /// <summary>
    /// 找到手牌中符合要求的是双连子
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindDoubleStraight(List<Card> allCards, int minWeight, int length)
    {
        List<Card> ret = new List<Card>();
        int counter = 0;
        List<int> indeies = new List<int>();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (i <= allCards.Count - 4)
            {
                int weight = (int)allCards[i].GetCardWeight;
                if (weight > minWeight)
                {
                    counter = 0;
                    indeies.Clear();

                    int circle = 0;
                    for (int j = i + 1; j < allCards.Count; j++)
                    {
                        if (allCards[j].GetCardWeight > CardWeight.One)
                            break;

                        if ((int)allCards[j].GetCardWeight - weight == counter)
                        {
                            circle++;
                            if (circle % 2 == 1)
                            {
                                counter++;
                            }
                            indeies.Add(j);
                        }

                        if (counter == length / 2)
                            break;
                    }
                }
            }
            if (counter == length / 2)
            {
                indeies.Insert(0, i);
                break;
            }

        }

        if (counter == length / 2)
        {
            for (int i = 0; i < indeies.Count; i++)
            {
                ret.Add(allCards[indeies[i]]);
            }
        }

        return ret;
    }
    /// <summary>
    /// 找到手牌中符合要求的是三连子
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected List<Card> FindTripleStraight(List<Card> allCards, int minWeight, int length)
    {
        List<Card> ret = new List<Card>();
        int counter = 0;
        List<int> indeies = new List<int>();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (i <= allCards.Count - 6)
            {
                int weight = (int)allCards[i].GetCardWeight;
                if (weight > minWeight)
                {
                    counter = 0;
                    indeies.Clear();

                    int circle = 0;
                    for (int j = i + 1; j < allCards.Count; j++)
                    {
                        if (allCards[j].GetCardWeight > CardWeight.One)
                            break;

                        if ((int)allCards[j].GetCardWeight - weight == counter)
                        {
                            circle++;
                            if (circle % 3 == 1)
                            {
                                counter++;
                            }
                            indeies.Add(j);
                        }

                        if (counter == length / 3)
                            break;
                    }
                }
            }
            if (counter == length / 3)
            {
                indeies.Insert(0, i);
                break;
            }

        }

        if (counter == length / 3)
        {
            for (int i = 0; i < indeies.Count; i++)
            {
                ret.Add(allCards[indeies[i]]);
            }
        }

        return ret;
    }
    protected List<Card> FindTripleStraightAndWing(List<Card> allCards, int minWeight, int length)
    {
        List<Card> ret = new List<Card>();
        if (length < 10 || length % 5 != 0)
        {
            return ret;
        }
        ret = FindTripleStraight(allCards, minWeight, (length / 5) * 3);
        if (ret.Count != 0)
        {
            List<Card> leftCards = GetAllCards(ret);
            ret.AddRange(leftCards.Take((length / 5) * 2).ToList());
        }
        return ret;
    }
    /// <summary>
    /// 三代二,电脑AI设计为带的一对(逻辑可优化)
    /// </summary>
    /// <param name="allCards"></param>
    /// <param name="weight"></param>
    /// <param name="equal"></param>
    /// <returns></returns>
    protected List<Card> FindThreeAndTwo(List<Card> allCards, int weight, bool equal)
    {
        List<Card> three = new List<Card>();
        if (three.Count < 5)
        {
            return three;
        }
        three = FindOnlyThree(allCards, weight, equal);
        if (three.Count != 0)
        {
            List<Card> leftCards = GetAllCards(three);
            List<Card> two = FindDouble(leftCards, (int)CardWeight.Three, true);

            three.AddRange(two);
        }
        else
            three.Clear();

        return three;

    }

    /// <summary>
    /// 三带一
    /// </summary>
    /// <param name="allCards"></param>
    /// <param name="weight"></param>
    /// <param name="equal"></param>
    /// <returns></returns>
    protected List<Card> FindThreeAndOne(List<Card> allCards, int weight, bool equal)
    {
        List<Card> three = new List<Card>();
        if (three.Count < 4)
        {
            return three;
        }
        three = FindOnlyThree(allCards, weight, equal);
        if (three.Count != 0)
        {
            List<Card> leftCards = GetAllCards(three);
            List<Card> one = FindSingle(leftCards, (int)CardWeight.Three, true);
            three.AddRange(one);
        }
        else
            three.Clear();

        return three;
    }
    /// <summary>
    /// 四带三
    /// </summary>
    /// <param name="allCards"></param>
    /// <param name="weight"></param>
    /// <param name="equal"></param>
    /// <returns></returns>
    protected List<Card> FindFourAndThree(List<Card> allCards, int weight, bool equal)
    {
        List<Card> four = new List<Card>();
        if (allCards.Count < 7)
        {
            return four;
        }
        four = FindBoom(allCards, weight, equal);
        if (four.Count != 0)
        {
            List<Card> leftCards = GetAllCards(four);
            List<Card> one_01 = FindSingle(leftCards, (int)CardWeight.Three, true);
            four.AddRange(one_01);
            leftCards = GetAllCards(four);
            List<Card> one_02 = FindSingle(leftCards, (int)CardWeight.Three, true);
            four.AddRange(one_02);
            leftCards = GetAllCards(one_02);
            List<Card> one_03 = FindSingle(leftCards, (int)CardWeight.Three, true);
            four.AddRange(one_03);
        }
        else
        {
            four.Clear();
        }
        return four;
    }
    public  List<Card> FirstCard()
    {
        List<Card> ret = new List<Card>();
        for (int i = 12; i >= 5; i--)
        {
            ret = FindStraight(GetAllCards(), (int)CardWeight.Three, i, true);
            if (ret.Count != 0)
                break;
        }
        if (ret.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                ret = FindThreeAndTwo(GetAllCards(), i, true);
                if (ret.Count != 0)
                    break;
            }
        }
        if (ret.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                ret = FindThreeAndOne(GetAllCards(), i, true);
                if (ret.Count != 0)
                    break;
            }
        }

        if (ret.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                ret = FindOnlyThree(GetAllCards(), i, true);
                if (ret.Count != 0)
                    break;
            }
        }

        if (ret.Count == 0)
        {
            for (int i = 0; i < 24; i += 2)
            {
                ret = FindDouble(GetAllCards(), i, true);
                if (ret.Count != 0)
                    break;
            }
        }

        if (ret.Count == 0)
        {
            ret = FindSingle(GetAllCards(), (int)CardWeight.Three, true);
        }

        return ret;
    }
    /// <summary>
    /// 要不起
    /// </summary>
    protected void ShowNotice()
    {
        Debug.Log("要不起");
        PlayAudioClip(CardsType.None ,null ,Sex.Man);
    }

    protected IEnumerator DisActiveNotice(GameObject notice)
    {
        yield return new WaitForSeconds(2.0f);
        //  computerNotice.SetActive(false);
    }
    void GetOutCard()
    {
        Transform tf =  this.BindingSource[20].transform;
    }
    // Use this for initialization
    void Start ()
    {
        Open();
        DealCards();
        StartCoroutine(SetMyAllhandCardTrue());
        if (firstPlayer == CharacterType.Player )
        {
            //SetBtnDisable(BindingSource[19], true);
            SetActive(BindingSource[19], true);
            BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_0"));
        }
        else
        {
            //SetBtnDisable(BindingSource[19], false);
            SetActive(BindingSource[19], true);
            BindingSource[19].GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("pdkSprite/{0}", "btn_outcard_2"));
        }
    }

    /// <summary>  
    /// 播放音效  
    /// </summary>  
    /// <param name="audioClip"></param>  
    /// <param name="volume"></param>  
    /// <param name="name"></param>  
    private void PlayAudioClip(CardsType cardsType, List<Card> selectedCardsList,Sex sex =Sex.None)
    {
        string audioClipPath;
        if (sex == Sex.Woman)
        {
            audioClipPath = "Music(UnUsed)/pdk/girl/";
        }
        else
        {
            audioClipPath = "Music(UnUsed)/pdk/boy/";
        }
       
        switch (cardsType)
        {
            case CardsType.Boom:
                audioClipPath += "Bomb";
                break;
            case CardsType.ThreeAndOne:
                audioClipPath += "3D1";
                break;
            case CardsType.ThreeAndTwo:
                audioClipPath += "3D2";
                break;
            case CardsType.Single:
                switch (selectedCardsList[0].GetCardWeight)
                {
                    case CardWeight.Three :
                        audioClipPath += "card_Size3";
                        break;
                    case CardWeight.Four:
                        audioClipPath += "card_Size4";
                        break;
                    case CardWeight.Five :
                        audioClipPath += "card_Size5";
                        break;
                    case CardWeight.Six :
                        audioClipPath += "card_Size6";
                        break;
                    case CardWeight.Seven:
                        audioClipPath += "card_Size7";
                        break;
                    case CardWeight.Eight :
                        audioClipPath += "card_Size8";
                        break;
                    case CardWeight.Nine :
                        audioClipPath += "card_Size9";
                        break;
                    case CardWeight.Ten:
                        audioClipPath += "card_Size10";
                        break;
                    case CardWeight.Jack:
                        audioClipPath += "card_Size11";
                        break;
                    case CardWeight.Queen:
                        audioClipPath += "card_Size12";
                        break;
                    case CardWeight.King:
                        audioClipPath += "card_Size13";
                        break;
                    case CardWeight.One:
                        audioClipPath += "card_Size14";
                        break;
                    case CardWeight.Two:
                        audioClipPath += "card_Size15";
                        break;

                }
                break;
            case CardsType.Double:
                switch (selectedCardsList[0].GetCardWeight)
                {
                    case CardWeight.Three:
                        audioClipPath += "double_Size3";
                        break;
                    case CardWeight.Four:
                        audioClipPath += "double_Size4";
                        break;
                    case CardWeight.Five:
                        audioClipPath += "double_Size5";
                        break;
                    case CardWeight.Six:
                        audioClipPath += "double_Size6";
                        break;
                    case CardWeight.Seven:
                        audioClipPath += "double_Size7";
                        break;
                    case CardWeight.Eight:
                        audioClipPath += "double_Size8";
                        break;
                    case CardWeight.Nine:
                        audioClipPath += "double_Size9";
                        break;
                    case CardWeight.Ten:
                        audioClipPath += "double_Size10";
                        break;
                    case CardWeight.Jack:
                        audioClipPath += "double_Size11";
                        break;
                    case CardWeight.Queen:
                        audioClipPath += "double_Size12";
                        break;
                    case CardWeight.King:
                        audioClipPath += "double_Size13";
                        break;
                    case CardWeight.One:
                        audioClipPath += "double_Size14";                      
                        break;
                }
                break;
            case CardsType.DoubleStraight:
                audioClipPath += "Doubleline";
                break;
            case CardsType.TripleStraight:
                audioClipPath += "Plane";
                break;
            case CardsType.Straight :
                audioClipPath += "Straight";
                break;
            case CardsType.OnlyThree:
                switch (selectedCardsList[0].GetCardWeight)
                {
                    case CardWeight.Three:
                        audioClipPath += "Three_Size3";
                        break;
                    case CardWeight.Four:
                        audioClipPath += "Three_Size4";
                        break;
                    case CardWeight.Five:
                        audioClipPath += "Three_Size5";
                        break;
                    case CardWeight.Six:
                        audioClipPath += "Three_Size6";
                        break;
                    case CardWeight.Seven:
                        audioClipPath += "Three_Size7";
                        break;
                    case CardWeight.Eight :
                        audioClipPath += "Three_Size8";
                        break;
                    case CardWeight.Nine:
                        audioClipPath += "Three_Size9";
                        break;
                    case CardWeight.Ten:
                        audioClipPath += "Three_Size10";
                        break;
                    case CardWeight.Jack:
                        audioClipPath += "Three_Size11";
                        break;
                    case CardWeight.Queen:
                        audioClipPath += "Three_Size12";
                        break;
                    case CardWeight.King:
                        audioClipPath += "Three_Size13";
                        break;
                    case CardWeight.One:
                        audioClipPath += "Three_Size14";
                        break;
                }                
                break;
            case CardsType.None:
                System.Random random = new System.Random();
                int dex = random.Next(4) +1;
                audioClipPath += (string.Format("pass{0}",dex));
                break;
            default:
                Debug.LogError("音效类型未处理=》" + cardsType);
                break;

        }
        AudioClip audioClip = Resources.Load<AudioClip>(audioClipPath);
        AudioSource audioSource = this.BindingSource[22].GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

    }


    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //UIManager.Instance.ShowDialog());
                //TooL.showTips("网络断开连接,点击确定重连", new TipsDialogCallBack(this.NetCloseBtn));
            }
        }
        OnButtonHandled();
    }
    private bool isDown = false;
    private bool isUp = false;
    private void OnButtonHandled()
    {
        //TODO : sunfei 退出游戏处理
        //if (this.uiStateManager.NextSubStateID == (int)UIState.UIState_None)//棋牌战斗内未处理
        //    return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isUp = true;
        }
        if (isDown && isUp)//后期请将此功能使用android原生开发
        {
            Debug.LogError("OnButtonHandled");
            GameObject tipsDialog = GameObject.Find("Canvas/addNode/TipsDialog");
            if (tipsDialog)
            {
                DestroyImmediate(tipsDialog);
            }
            else
            {
                TooL.showTips("退出游戏", () => Application.Quit());
            }
            isDown = false;
            isUp = false;

        }
    }
}
