using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;
using com.guojin.mj.net.message.game;

public class MJRePlayOpt
{

    protected int selfIndex;

    public MJRePlayOpt(int selfIndex)
    {
        this.selfIndex = selfIndex;
    }

    public int getSelfIndex()
    {
        return selfIndex;
    }
    public void setSelfIndex(int selfIndex)
    {
        this.selfIndex = selfIndex;
    }
}
public class MJRePlay
{
    private int no;
    private MJRePlayOpt opt;
    public int No
    {
        get
        {
            return no;
        }

        set
        {
            no = value;
        }
    }

    public MJRePlayOpt Opt
    {
        get
        {
            return opt;
        }

        set
        {
            opt = value;
        }
    }

    public MJRePlay()
    {

    }
    public MJRePlay(int no, MJRePlayOpt opt)
    {
        this.No = no;
        this.Opt = opt;
    }

    public String toString()
    {
        return "MJRePlay [no=" + No + ", opt=" + Opt + "]";
    }
}
public class MJRePlayAnGang : MJRePlayOpt
{
    private int pai;
    public MJRePlayAnGang(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayAnGang(int selfIndex, int pai) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.pai = pai;
    }
    public int getPai()
    {
        return pai;
    }
    public void setPai(int pai)
    {
        this.pai = pai;
    }
    public String toString()
    {
        return "MJRePlayAnGang [pai=" + pai + "]";
    }




}
public class MJRePlayChapterInfo : MJRePlayOpt
{
    Dictionary<int, List<int>> user_shouPais = new Dictionary<int, List<int>>();
    public MJRePlayChapterInfo(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayChapterInfo(int selfIndex, Dictionary<int, List<int>> user_shouPais) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.user_shouPais = user_shouPais;
    }

    public void addUserShouPai(int index, List<int> shouPais)
    {
        user_shouPais.Add(index, shouPais);
    }

    public Dictionary<int, List<int>> getUser_shouPais()
    {
        return user_shouPais;
    }

    public void setUser_shouPais(Dictionary<int, List<int>> user_shouPais)
    {
        this.user_shouPais = user_shouPais;
    }
    public String toString()
    {
        return "MJRePalyChapterInfo [user_shouPais=" + user_shouPais + "]";
    }

}
public class MJRePlayDaPai : MJRePlayOpt
{

    private int pai;
    public MJRePlayDaPai(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayDaPai(int selfIndex, int pai) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.pai = pai;
    }
    public int getPai()
    {
        return pai;
    }
    public void setPai(int pai)
    {
        this.pai = pai;
    }
    public String toString()
    {
        return "MJRePalyDaPai [pai=" + pai + "]";
    }

}
public class MJRePlayHu : MJRePlayOpt
{

    private Dictionary<int, int> winScore;
    private Dictionary<int, int> gangScore;
    private List<int> winner_shoupai;

    public MJRePlayHu(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayHu(int selfIndex, Dictionary<int, int> winScore, Dictionary<int, int> gangScore,
            List<int> winner_shoupai) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.winScore = winScore;
        this.gangScore = gangScore;
        this.winner_shoupai = winner_shoupai;
    }
    public Dictionary<int, int> getWinScore()
    {
        return winScore;
    }
    public void setWinScore(Dictionary<int, int> winScore)
    {
        this.winScore = winScore;
    }
    public Dictionary<int, int> getGangScore()
    {
        return gangScore;
    }
    public void setGangScore(Dictionary<int, int> gangScore)
    {
        this.gangScore = gangScore;
    }
    public List<int> getWinner_shoupai()
    {
        return winner_shoupai;
    }

    public void setWinner_shoupai(List<int> winner_shoupai)
    {
        this.winner_shoupai = winner_shoupai;
    }
    public String toString()
    {
        return "MJRePlayHu [winScore=" + winScore + ", gangScore=" + gangScore + ", winner_shoupai=" + winner_shoupai
                + "]";
    }
}

public class MJRePlayMingGang : MJRePlayOpt
{

    private int pai;
    private int gang_index;
    public MJRePlayMingGang(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayMingGang(int selfIndex, int pai, int gang_index) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.pai = pai;
        this.gang_index = gang_index;
    }
    public int getPai()
    {
        return pai;
    }
    public void setPai(int pai)
    {
        this.pai = pai;
    }
    public int getGang_index()
    {
        return gang_index;
    }
    public void setGang_index(int gang_index)
    {
        this.gang_index = gang_index;
    }
    public String toString()
    {
        return "MJRePalyMingGang [pai=" + pai + ", gang_index=" + gang_index + "]";
    }
}
public class MJRePlayOffLine : MJRePlayOpt
{
    public MJRePlayOffLine(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
}
public class MJRePlayOnLine : MJRePlayOpt
{
    public MJRePlayOnLine(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
}
public class MJRePlayPengPai : MJRePlayOpt
{
    private int pai;
    private int peng_index;
    public MJRePlayPengPai(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayPengPai(int selfIndex, int pai, int peng_index) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.pai = pai;
        this.peng_index = peng_index;
    }
    public int getPai()
    {
        return pai;
    }
    public void setPai(int pai)
    {
        this.pai = pai;
    }
    public int getPeng_index()
    {
        return peng_index;
    }
    public void setPeng_index(int peng_index)
    {
        this.peng_index = peng_index;
    }
    public String toString()
    {
        return "MJRePlayPengPai [pai=" + pai + ", peng_index=" + peng_index + "]";
    }

}
public class MJRePlayQiPai : MJRePlayOpt
{
    private int pai;
    public MJRePlayQiPai(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }

    public MJRePlayQiPai(int selfIndex, int pai) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.pai = pai;
    }
    public int getPai()
    {
        return pai;
    }
    public void setPai(int pai)
    {
        this.pai = pai;
    }
    public String toString()
    {
        return "MJRePalyQiPai [pai=" + pai + "]";
    }
}
public class MJRePlayRoomUserInfo : MJRePlayOpt
{

    private String roomNO;
    List<GameUserInfo> users = new List<GameUserInfo>();
    private Dictionary<String, String> options = new Dictionary<String, String>();
    public MJRePlayRoomUserInfo(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayRoomUserInfo(int selfIndex, String roomNO, List<GameUserInfo> users, Dictionary<String, String> options) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.roomNO = roomNO;
        this.users = users;
        this.options = options;
    }
    public String getRoomNO()
    {
        return roomNO;
    }
    public void setRoomNO(String roomNO)
    {
        this.roomNO = roomNO;
    }
    public Dictionary<String, String> getOptions()
    {
        return options;
    }
    public void setOptions(Dictionary<String, String> options)
    {
        this.options = options;
    }
    public List<GameUserInfo> getUsers()
    {
        return users;
    }
    public void setUsers(List<GameUserInfo> users)
    {
        this.users = users;
    }
    public String toString()
    {
        return "MJRePlayRoomUserInfo [roomNO=" + roomNO + ", users=" + users + ", options=" + options + "]";
    }
}
//public class MJRePlayUtil
//{
//    public const int ROOM_USER_INFO = 0; //同步用户信息（头像，昵称，积分等）
//    public const int XUAN_PAO = 1;   //选跑
//    public const int CHAPTER_INFO = 2;   //同步用户牌信息（每个人发的手牌）
//    public const int QI_PAI = 3; //用户起一张牌
//    public const int DA_PAI = 4; //用户打一张牌
//    public const int PENG_PAI = 5;   //用户碰一张牌
//    public const int MING_GANG_PAI = 6;  //用户明杠一张牌
//    public const int AN_GANG_PAI = 7;    //用户暗杠一张牌
//    public const int HU_PAI = 8; //用户胡了，同步四人积分和胡牌情况

//    public const int USER_OFFLINE = 9;   //用户下线了
//    public const int USER_ONLINE = 10;   //用户下线了
//    public static MJRePlay roomUserInfo(GameUserInfo user, GameRoomInfo roomInfo, Config config)
//    {
//        List<GameUserInfo> users = new List<GameUserInfo>();

//        for (int i = 0; i < roomInfo.sceneUser.Count; i++)
//        {
//            users.Add(roomInfo.sceneUser[i]);

//        }
//        MJRePlayOpt rePlayopt = new MJRePlayRoomUserInfo(user.locationIndex, roomInfo.roomCheckId, users, config.getOptions());
//        MJRePlay rePlay = new MJRePlay(ROOM_USER_INFO, rePlayopt);
//        return rePlay;
//    }

//    public static MJRePlay xuanPao(GameUserInfo user, int xuanPao)
//    {
//        MJRePlayOpt rePlayOpt = new MJRePlayXuanPao(user.locationIndex, xuanPao);
//        return new MJRePlay(XUAN_PAO, rePlayOpt);
//    }


//    public static MJRePlay chapterInfo(GameUserInfo user, MajiangChapterMsg chapter)
//    {
//        Dictionary<int, List<int>> user_shoupais = new Dictionary<int, List<int>>();
//        List<UserPlaceMsg> userPlaces = chapter.userPlace;
//        for (int i = 0; i < userPlaces.Count; i++)
//        {
//            UserPlaceMsg userPlace = userPlaces[i];

//            user_shoupais.Add(user.locationIndex, userPlace.shouPai)             

//    }
//    MJRePlayOpt opt = new MJRePlayChapterInfo(user.locationIndex, user_shoupais);
//    MJRePlay rePlay = new MJRePlay(CHAPTER_INFO, opt);
//		return rePlay;
//	}


//public static MJRePlay faPai(int index, int pai)
//{
//    MJRePlayOpt opt = new MJRePlayQiPai(index, pai);
//    MJRePlay rePlay = new MJRePlay(QI_PAI, opt);
//    return rePlay;
//}

//public static MJRePlay daPai(int index, int pai)
//{
//    MJRePlayOpt opt = new MJRePlayDaPai(index, pai);
//    MJRePlay rePlay = new MJRePlay(DA_PAI, opt);
//    return rePlay;
//}

//public static MJRePlay anGang(int index, int pai)
//{
//    MJRePlayOpt opt = new MJRePlayAnGang(index, pai);
//    MJRePlay rePlay = new MJRePlay(AN_GANG_PAI, opt);
//    return rePlay;
//}

//public static MJRePlay mingGang(int index, int pai, int otherIndex)
//{
//    MJRePlayOpt opt = new MJRePlayMingGang(index, pai, otherIndex);
//    MJRePlay rePlay = new MJRePlay(MING_GANG_PAI, opt);
//    return rePlay;
//}

//public static MJRePlay peng(int index, int pai, int otherIndex)
//{
//    MJRePlayOpt opt = new MJRePlayPengPai(index, pai, otherIndex);
//    MJRePlay rePlay = new MJRePlay(PENG_PAI, opt);
//    return rePlay;
//}

//public static MJRePlay hu(int index, GameChapterEnd endMsg, MajiangChapterMsg chapter)
//{
//    Dictionary<int, int> winScore = new Dictionary<int, int>();
//        Dictionary<int, int> gangScore = new Dictionary<int, int>();
//    List<int> winner_shoupai = chapter.userPlace[index]
//            .shouPai
//            .stream()
//            .map(r->{
//        int pai = r.getIndex();
//        return pai;
//    }).collect(Collectors.toCollection(ArrayList < Integer >::new));



//    MJRePlayOpt opt = new MJRePlayHu(index, winScore, gangScore, winner_shoupai);

//    List<GameFanResult> results = endMsg.fanResults;
//    for (int i = 0; i < results.Count; i++)
//    {
//            GameFanResult result = results[i];
//        winScore.Add(i, result.score);
//        gangScore.Add(i, result.guaFengXiaYu);
//    }

//    MJRePlay rePlay = new MJRePlay(HU_PAI, opt);
//    return rePlay;
//}
//}
public class MJRePlayXuanPao : MJRePlayOpt
{
    private int num;

    public MJRePlayXuanPao(int selfIndex) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
    }
    public MJRePlayXuanPao(int selfIndex, int num) : base(selfIndex)
    {
        base.selfIndex = selfIndex;
        this.num = num;
    }
    public int getNum()
    {
        return num;
    }
    public void setNum(int num)
    {
        this.num = num;
    }
    public String toString()
    {
        return "MJRePalyXuanPao [num=" + num + "]";
    }
}


