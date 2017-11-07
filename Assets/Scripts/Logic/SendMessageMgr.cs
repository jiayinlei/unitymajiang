using com.guojin.mj.net.message.club;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageMgr : MonoBehaviour {
    /// <summary>
    /// 获取战绩
    /// </summary>
    public static void GetRoomHistory()
    {
        com.guojin.mj.net.message.login.RoomHistoryList rhl = new com.guojin.mj.net.message.login.RoomHistoryList();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(rhl));
    }
    /// <summary>
    /// -加入房间-
    /// roomID 房间号
    /// </summary>
    /// <param name="roomID"></param>
    public static void JoinRoom(string roomID)
    {
        if (CheckID(roomID))
        {
            com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(roomID);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
        }
    }
    /// <summary>
    /// -加入俱乐部-
    /// ClubID 俱乐部号
    /// </summary>
    /// <param name="ClubID"></param>
    public static void JoinClub(string ClubID)
    {
        if (CheckID(ClubID))
        {
            com.guojin.mj.net.message.club.JoinClub joinClubReq = new com.guojin.mj.net.message.club.JoinClub();
            joinClubReq.ClubId = ClubID;
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(joinClubReq));
        }
    }

 #region//检查参数是否合法的逻辑代码写在下面
    /// <summary>
    /// 检查ID是否合法
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static bool CheckID(string ID)
    {
        int id;
        if (int.TryParse(ID, out id) && ID.Length == 6)
        {
            return true;
        }
        return false;          
    }
#endregion
}
