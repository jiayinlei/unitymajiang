using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/* ***********************************************
 * Describe:扑克牌
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/26 14:57:49 
 * FileName: PokerCardType 
 * Version: V1.0.1 
 * ***********************************************/

/// 花色
enum PokerCardKinds {
    DEFAULT,
    DIAMONDS,   //方片
    CLUBS,      //梅花
    HEARTS,     //红心
    SPADES,     //黑桃
}


/// 牌形
enum PokerCardType {
    DEFAULT,//默认
    NONE,// 无牛
    N1,//牛1
    N2,//牛2
    N3,//牛3
    N4,//牛4
    N5,//牛5
    N6,//牛6
    N7,//牛7
    N8,//牛8
    N9,//牛9
    NN,//牛牛
    HN4,//四花牛
    HN5,//五花牛
    XN5,//五小牛
    BOMB//炸弹
}

