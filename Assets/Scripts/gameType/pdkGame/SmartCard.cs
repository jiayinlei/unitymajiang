using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 电脑出牌AI
/// </summary>
public abstract class SmartCard : MonoBehaviour
{
    //protected GameObject computerNotice;
    // Use this for initialization
    void Start()
    {
        //computerNotice = transform.Find("ComputerNotice").gameObject;
        OrderController.Instance.smartCard += AutoDiscardCard;
    }

    /// <summary>
    /// 自动出牌
    /// </summary>
    protected void AutoDiscardCard(bool isNone, bool isMyTurn)
    {
        HandCards t = gameObject.GetComponent<HandCards>();
        if (OrderController.Instance.Type == t.cType)
        {
            StartCoroutine(DelayDiscardCard(isNone, isMyTurn));

        }

    }

    /// <summary>
    /// 一手牌
    /// </summary>
    /// <returns></returns>
    public abstract List<Card> FirstCard();

    /// <summary>
    /// 延时出牌
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator DelayDiscardCard(bool isNone,bool isMyTurn)
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
                    DiscardCards(discardCards_00, GetSprite(discardCards_00));
                }
                break;
            case CardsType.Double:
                List<Card> conformCards_01 = FindDouble(GetAllCards(), deskWeight, false);
                if (conformCards_01.Count != 0 && isMyTurn !=true)
                {
                    RemoveCards(conformCards_01);
                    DiscardCards(conformCards_01, GetSprite(conformCards_01));
                }
                else if ((conformCards_01.Count != 0 && isMyTurn == true))
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true ,false );
                }
                else if (conformCards_01.Count == 0 &&isMyTurn == true)
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true ,false );
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
                if (conformCards_02.Count != 0 && isMyTurn !=true)
                {
                    RemoveCards(conformCards_02);
                    DiscardCards(conformCards_02, GetSprite(conformCards_02));
                }
                else if (conformCards_02.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
            case CardsType.OnlyThree:
                List<Card> conformCards_03 = FindOnlyThree(GetAllCards(), deskWeight, false);
                if (conformCards_03.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(conformCards_03);
                    DiscardCards(conformCards_03, GetSprite(conformCards_03));
                }
                else if (conformCards_03.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                }
                else if (conformCards_03.Count == 0 && isMyTurn == true)
                {
                    List<Card> boom = FindBoom(GetAllCards(), 0, true);
                    if (boom.Count != 0)
                    {
                        //Interaction interaction = FindObjectOfType<Interaction>();
                        //interaction.ActiveCardButton(true, false);
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
                    DiscardCards(conformCards_04, GetSprite(conformCards_04));
                }
                else if (conformCards_04.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                    DiscardCards(discardCards_06, GetSprite(discardCards_06));
                }
                else if (discardCards_06.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                        //interaction.ActiveCardButton(true, false);
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
                    DiscardCards(discardCards_08, GetSprite(discardCards_08));
                }
                else if (discardCards_08.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                        //interaction.ActiveCardButton(true, false);
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
                    DiscardCards(conformCards09, GetSprite(conformCards09));
                }
                else if (conformCards09.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                        //interaction.ActiveCardButton(true, false);
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
                    DiscardCards(boom_01, GetSprite(boom_01));
                }
                else if (boom_01.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                        //interaction.ActiveCardButton(true, false);
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case  CardsType.Boom:
                List<Card> boom_0 = FindBoom(GetAllCards(), deskWeight, true);
                if (boom_0.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(boom_0);
                    DiscardCards(boom_0, GetSprite(boom_0));
                }
                else if (boom_0.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
                }
                else
                {
                    OrderController.Instance.Turn();
                    ShowNotice();
                }
                break;
            case CardsType.FourAndThree:
                List<Card> boom_09 = FindFourAndThree(GetAllCards(), deskWeight, true);
                if (boom_09.Count !=0 && isMyTurn != true)
                {
                    RemoveCards(boom_09);
                    DiscardCards(boom_09, GetSprite(boom_09));
                }
                else if (boom_09.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                        //interaction.ActiveCardButton(true, false);
                    }
                    else
                    {
                        OrderController.Instance.Turn();
                        ShowNotice();
                    }
                }
                break;
            case CardsType.TripleStraightAndWing :
                List<Card> boom_10 = FindTripleStraightAndWing(GetAllCards(), DeskCardsCache.Instance.FlyWingMinWeight, DeskCardsCache.Instance.CardsCount);
                if (boom_10.Count != 0 && isMyTurn != true)
                {
                    RemoveCards(boom_10);
                    DiscardCards(boom_10, GetSprite(boom_10));
                }
                else if (boom_10.Count != 0 && isMyTurn == true)
                {
                    //Interaction interaction = FindObjectOfType<Interaction>();
                    //interaction.ActiveCardButton(true, false);
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
                        //interaction.ActiveCardButton(true, false);
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

            HandCards player = gameObject.GetComponent<HandCards>();
            //如果符合将牌从手牌移到出牌缓存区
            DeskCardsCache.Instance.Clear();
            DeskCardsCache.Instance.Rule = type;

            for (int i = 0; i < selectedSpriteList.Count; i++)
            {
                DeskCardsCache.Instance.AddCard(selectedSpriteList[i].Poker);
                selectedSpriteList[i].transform.parent = GameObject.Find("Desk").transform;
                selectedSpriteList[i].Poker = selectedSpriteList[i].Poker;
            }

            DeskCardsCache.Instance.Sort();
            GameController.AdjustCardSpritsPosition(CharacterType.Desk);
            GameController.AdjustCardSpritsPosition(player.cType);

            GameController.UpdateLeftCardsCount(player.cType, player.CardsCount);

            if (player.CardsCount == 0)
            {
                GameObject.Find("GameController").GetComponent<GameController>().GameOver();
            }
            else
            {
                OrderController.Instance.Biggest = player.cType;
                OrderController.Instance.Turn();
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
        HandCards allCards = gameObject.GetComponent<HandCards>();
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
        HandCards t = gameObject.GetComponent<HandCards>();
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
        HandCards allCards = gameObject.GetComponent<HandCards>();

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
        for (int i = 0; i < allCards.Count; i++)
        {
            if (i < allCards.Count - 1)
            {
                if (allCards[i].GetCardWeight == allCards[i + 1].GetCardWeight)
                {
                    int totalWeight = (int)allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight;
                    if (equal)
                    {
                        if (totalWeight >= weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i + 1]);
                            break;
                        }
                    }
                    else
                    {
                        if (totalWeight > weight)
                        {
                            ret.Add(allCards[i]);
                            ret.Add(allCards[i + 1]);
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
                if ((int)allCards[i].GetCardWeight >= weight)
                {
                    ret.Add(allCards[i]);
                    break;
                }
            }
            else
            {
                if ((int)allCards[i].GetCardWeight > weight)
                {
                    ret.Add(allCards[i]);
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
    protected List<Card> FindTripleStraightAndWing (List<Card> allCards, int minWeight, int length)
    {
        List<Card> ret = new List<Card>();
        if (length < 10 || length %5 !=0)
        {
            return ret;
        }
        ret = FindTripleStraight(allCards, minWeight,(length/5)*3);
        if (ret.Count!=0)
        {
            List<Card> leftCards = GetAllCards(ret);
            ret.AddRange(leftCards .Take((length/5)*2).ToList());
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
        if (three.Count<4)
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
        if (allCards.Count<7)
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
    /// <summary>
    /// Pass label
    /// </summary>
    protected void ShowNotice()
    {
       // computerNotice.SetActive(true);
        //computerNotice.GetComponent<TweenAlpha>().ResetToBeginning();
        //computerNotice.GetComponent<TweenAlpha>().PlayForward();
     //   StartCoroutine(DisActiveNotice(computerNotice));
    }

    protected IEnumerator DisActiveNotice(GameObject notice)
    {
        yield return new WaitForSeconds(2.0f);
      //  computerNotice.SetActive(false);
    }
}
