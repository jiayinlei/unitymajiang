using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 牌库//不能继承Mono,构造
/// </summary>
public class Deck
{
    private static Deck instance;
    private List<Card> library;
    private List<Card> standardLibrary;
    private CharacterType ctype;

    public static Deck Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Deck();
            }
            return instance;
        }
    }

    /// <summary>
    /// 获取牌库中牌的数量
    /// </summary>
    public int CardsCount
    {
        get { return library.Count; }
    }

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Card this[int index]
    {
        get
        {
            return library[index];
        }
    }

    /// <summary>
    /// 私有构造
    /// </summary>
    private Deck()
    {
        library = new List<Card>();
        standardLibrary = new List<Card>();
        ctype = CharacterType.Library;
        CreateDeck();
    }

    /// <summary>
    /// 创建一副牌
    /// </summary>
    void CreateDeck()
    {
        ////创建普通扑克-老版
        //for (int color = 0; color < 4; color++)
        //{
        //    for (int value = 0; value < 13; value++)
        //    {
        //        Weight w = (Weight)value;
        //        Suits s = (Suits)color;
        //        string name = s.ToString() + w.ToString();
        //        Card card = new Card(name, w, s, ctype);
        //        if ((w == Weight.Two && s != Suits.Spade) || (w == Weight.One && s == Suits.Spade))
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            library.Add(card);
        //        }

        //    }
        //}
        int dex = 0;
        //创建普通扑克-新版
        for (int i = 11; i < 13; i++)
        {
            for (int color = 0; color < 4; color++)
            {

                CardWeight w = (CardWeight)i;
                CardSuits s = (CardSuits)color;
                string name = s.ToString() + w.ToString();
                Card card0 = new Card(name, w, s, ctype, dex,false);
                standardLibrary.Add(card0);
                dex++;
                
            }
        }
        for (int i = 0; i < 11; i++)
        {
            for (int color = 0; color < 4; color++)
            {

                CardWeight w = (CardWeight)i;
                CardSuits s = (CardSuits)color;
                string name = s.ToString() + w.ToString();
                Card card1 = new Card(name, w, s, ctype, dex,false );
                standardLibrary.Add(card1);
                dex++;

            }
        }


        Card card2 = new Card("SJoker", CardWeight.SJoker, CardSuits.None, ctype, dex,false );
        standardLibrary.Add(card2);
        dex++;
        Card card3 = new Card("LJoker", CardWeight.LJoker, CardSuits.None, ctype, dex,false );
        standardLibrary.Add(card3);

        standardLibrary.ForEach(i => {
            if (i.GetCardIndex != 3 && i.GetCardIndex !=4 
            && i.GetCardIndex != 5&& i.GetCardIndex != 6
            && i.GetCardIndex != 52&& i.GetCardIndex != 53)
            {
                library.Add(i);
            }
        });
        ////打印测试
        //library.ForEach(i=> {
        //    Debug.Log(i.GetCardIndex);
        //});

    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        if (CardsCount == 48)
        {
            System.Random random = new System.Random();
            List<Card> newList = new List<Card>();
            foreach (Card item in library)
            {
                newList.Insert(random.Next(newList.Count + 1), item);
            }

            library.Clear();

            foreach (Card item in newList)
            {
                library.Add(item);
            }

            newList.Clear();
        }
    }

    /// <summary>
    /// 发牌
    /// </summary>
    public Card Deal()
    {
        Card ret = library[library.Count - 1];
        library.Remove(ret);
        return ret;
    }

    /// <summary>
    /// 向牌库中添加牌
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(Card card)
    {
        card.Attribution = ctype;
        library.Add(card);
    }

}
