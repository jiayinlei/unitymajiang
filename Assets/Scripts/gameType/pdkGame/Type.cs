using UnityEngine;
using System.Collections;

/// <summary>
/// 角色类型
/// </summary>
public enum CharacterType
{
    Library = 0,
    Player,
    PlayerOne,
    PlayerTwo,
    Desk
}


/// <summary>
/// 花色
/// </summary>
public enum CardSuits
{
    Diamond,//方块
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    None
}
/// <summary>
/// 卡牌权值
/// </summary>
public enum CardWeight
{
    Three = 0,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker,
}
public  enum Sex
{
    Man = 0,
    Woman,
    None
}
/// <summary>
/// 卡牌权值
/// </summary>
public enum newCardWeight
{
    Diamond_One = 0,
    Club_One,
    Heart_One,
    Spade_One ,
    Diamond_Two ,
    Club_Two,
    Heart_Two,
    Spade_Two ,
    Diamond_Three ,
    Club_Three ,
    Heart_Three,
    Spade_Three ,
    Diamond_Four ,
    Club_Four,
    Heart_Four,
    Spade_Four ,
    Diamond_Five ,
    Club_Five,
    Heart_Five,
    Spade_Five ,
    Diamond_Six ,
    Club_Six,
    Heart_Six,
    Spade_Six ,
    Diamond_Seven ,
    Club_Seven,
    Heart_Seven,
    Spade_Seven ,
    Diamond_Eight ,
    Club_Eight,
    Heart_Eight,
    Spade_Eight ,
    Diamond_Nine ,
    Club_Nine,
    Heart_Nine,
    Spade_Nine ,
    Diamond_Ten ,
    Club_Ten,
    Heart_Ten,
    Spade_Ten ,
    Diamond_Jack ,
    Club_Jack,
    Heart_Jack,
    Spade_Jack ,
    Diamond_Queen ,
    Club_Queen,
    Heart_Queen,
    Spade_Queen ,
    Diamond_King ,
    Club_King,
    Heart_King,
    Spade_King ,
    SJoker ,
    LJoker 
}

/// <summary>
/// 卡牌权值
/// </summary>
public enum NewCardWeight
{
    Diamond_One = 11,
    Club_One,
    Heart_One,
    Spade_One = 11,
    Diamond_Two = 12,
    Club_Two,
    Heart_Two,
    Spade_Two = 12,
    Diamond_Three = 0,
    Club_Three,
    Heart_Three,
    Spade_Three = 0,
    Diamond_Four = 1,
    Club_Four,
    Heart_Four,
    Spade_Four = 1,
    Diamond_Five = 2,
    Club_Five,
    Heart_Five,
    Spade_Five = 2,
    Diamond_Six = 3,
    Club_Six,
    Heart_Six,
    Spade_Six = 3,
    Diamond_Seven = 4,
    Club_Seven,
    Heart_Seven,
    Spade_Seven = 4,
    Diamond_Eight = 5,
    Club_Eight,
    Heart_Eight,
    Spade_Eight = 5,
    Diamond_Nine = 6,
    Club_Nine,
    Heart_Nine,
    Spade_Nine = 6,
    Diamond_Ten = 7,
    Club_Ten,
    Heart_Ten,
    Spade_Ten = 7,
    Diamond_Jack = 8,
    Club_Jack,
    Heart_Jack,
    Spade_Jack = 8,
    Diamond_Queen = 9,
    Club_Queen,
    Heart_Queen,
    Spade_Queen = 9,
    Diamond_King = 10,
    Club_King,
    Heart_King,
    Spade_King = 10,
    SJoker = 13,
    LJoker = 14
}
/// <summary>
/// 身份
/// </summary>
public enum Identity
{
    Farmer,
    Landlord
}

/// <summary>
/// 出牌类型
/// </summary>
public enum CardsType
{
    //未知类型
    None,
    //炸弹
    Boom,
    //三个不带
    OnlyThree,
    //三个带一
    ThreeAndOne,
    //三个带二
    ThreeAndTwo,
    //顺子 五张或更多的连续单牌
    Straight,
    //双顺 三对或更多的连续对牌
    DoubleStraight,
    //三顺 二个或更多的连续三张牌
    TripleStraight,
    //对子
    Double,
    //单个
    Single,
    //飞机带翅膀
    TripleStraightAndWing,
    //四带三
    FourAndThree
}

/// <summary>
/// 存储数据类型
/// </summary>
[System.Serializable]
public class CardGameData
{
    public int playerIntegration;
    public int computerOneIntegration;
    public int computerTwoIntegration;
}