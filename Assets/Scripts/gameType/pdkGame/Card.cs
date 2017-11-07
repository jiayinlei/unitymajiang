using UnityEngine;
using System.Collections;

/// <summary>
/// 牌类
/// </summary>
public class Card 
{
    private readonly string cardName;
    private readonly int index;
    private readonly CardWeight weight;
    private readonly CardSuits color;
    private CharacterType belongTo;
    private readonly bool isSelect;


    public Card(string name, CardWeight weight, CardSuits color, CharacterType belongTo,int index,bool isSelect)
    {
        cardName = name;
        this.weight = weight;
        this.color = color;
        this.belongTo = belongTo;
        this.index = index;
        this.isSelect = isSelect;
    }
    /// <summary>
    /// 返回牌名
    /// </summary>
    public string GetCardName
    {
        get { return cardName; }
    }
    public bool IsSelect
    {
        get { return isSelect; }
    }
    /// <summary>
    /// 返回权值
    /// </summary>
    public CardWeight GetCardWeight
    {
        get { return weight; }
    }
    public int GetCardIndex
    {
        get { return index ; }
    }
    /// <summary>
    /// 返回花色
    /// </summary>
    public CardSuits GetCardSuit
    {
        get { return color; }
    }



    /// <summary>
    /// 牌的归属
    /// </summary>
    public CharacterType Attribution
    {
        set { belongTo = value; }
        get { return belongTo; }
    }

}
