using com.guojin.core.io.message;
using com.guojin.mj.net.message.login;
using com.guojin.core.utils;
using UnityEngine;

namespace com.guojin.mj.net.message
{
	public class MessageFactoryImpi : Singleton<MessageFactoryImpi>, MessageFactory
	{
		private string[,] vector = new string[8, 60];
		
		public MessageFactoryImpi()
		{
            initDNMessageFactory();
            vector[7, 0] = "com.guojin.mj.net.message.login.CreateRoom";
            vector[7, 1] = "com.guojin.mj.net.message.login.CreateRoomRet";
            vector[7, 2] = "com.guojin.mj.net.message.login.DelRoom";
            vector[7, 3] = "com.guojin.mj.net.message.login.DelRoomRet";
            vector[7, 4] = "com.guojin.mj.net.message.login.ExitRoom";
            vector[7, 5] = "com.guojin.mj.net.message.login.ExitRoomRet";
            vector[7, 6] = "com.guojin.mj.net.message.login.JoinRoom";
            vector[7, 7] = "com.guojin.mj.net.message.login.JoinRoomRet";
            vector[7, 8] = "com.guojin.mj.net.message.login.Login";
            vector[7, 9] = "com.guojin.mj.net.message.login.LoginError";
            vector[7, 10] = "com.guojin.mj.net.message.login.Login_Ret";
            vector[7, 11] = "com.guojin.mj.net.message.login.Notice";
            vector[7, 12] = "com.guojin.mj.net.message.login.OptionEntry";
            vector[7, 13] = "com.guojin.mj.net.message.login.Pay";
            vector[7, 14] = "com.guojin.mj.net.message.login.Ping";
            vector[7, 15] = "com.guojin.mj.net.message.login.Pong";
            vector[7, 16] = "com.guojin.mj.net.message.login.Radio";
            vector[7, 17] = "com.guojin.mj.net.message.login.RepeatLoginRet";
            vector[7, 18] = "com.guojin.mj.net.message.login.RoomHistory";
            vector[7, 19] = "com.guojin.mj.net.message.login.RoomHistoryList";
            vector[7, 20] = "com.guojin.mj.net.message.login.RoomHistoryListRet";
            vector[7, 21] = "com.guojin.mj.net.message.login.SendAuthCode";
            vector[7, 22] = "com.guojin.mj.net.message.login.SendAuthCodeRet";
            vector[7, 23] = "com.guojin.mj.net.message.login.StartGame";
            vector[7, 24] = "com.guojin.mj.net.message.login.SysSetting";

            vector[7, 25] = "com.guojin.mj.net.message.login.SysSetting";
            vector[7, 26] = "com.guojin.mj.net.message.login.SysSetting";
            vector[7, 27] = "com.guojin.mj.net.message.login.WXPay";
            vector[7, 28] = "com.guojin.mj.net.message.login.WXPayRet";
            vector[7, 29] = "com.guojin.mj.net.message.login.PayBack";
            vector[7, 30] = "com.guojin.mj.net.message.login.SysSetting";
            vector[7, 31] = "com.guojin.mj.net.message.login.SysSetting";
            vector[7, 32] = "com.guojin.mj.net.message.login.WeiXinShow";

            vector[7, 33] = "com.guojin.mj.net.message.club.JoinClub";
            vector[7, 34] = "com.guojin.mj.net.message.club.JoinClubRet";
            vector[7, 35] = "com.guojin.mj.net.message.login.Record";//获胜记录
            vector[7, 36] = "com.guojin.mj.net.message.club.ClubInfo";
            vector[7, 37] = "com.guojin.mj.net.message.login.Report";
            vector[7, 38] = "com.guojin.mj.net.message.login.Playback";
            vector[7, 39] = "com.guojin.mj.net.message.login.PlaybackRet";
            vector[7, 40] = "com.guojin.mj.net.message.login.AlreadyRoomID";
            vector[7, 41] = "com.guojin.mj.net.message.login.AckLocation";






            vector[1, 0] = "com.guojin.mj.net.message.game.GameChapterEnd";//牌局结束
            vector[1, 1] = "com.guojin.mj.net.message.game.GameChapterStart";//开始游戏
            vector[1, 2] = "com.guojin.mj.net.message.game.GameChapterStartRet";
            vector[1, 3] = "com.guojin.mj.net.message.game.GameDelRoom";
            vector[1, 4] = "com.guojin.mj.net.message.game.GameExitUser";
            vector[1, 5] = "com.guojin.mj.net.message.game.GameFanResult";
            vector[1, 6] = "com.guojin.mj.net.message.game.GameJoinRoom";
            vector[1, 7] = "com.guojin.mj.net.message.game.GameRoomInfo";
            vector[1, 8] = "com.guojin.mj.net.message.game.GameUserInfo";
            vector[1, 9] = "com.guojin.mj.net.message.game.MajiangChapterMsg";
            vector[1, 10] = "com.guojin.mj.net.message.game.OperationCPGH";
            vector[1, 11] = "com.guojin.mj.net.message.game.OperationCPGHRet";
            vector[1, 12] = "com.guojin.mj.net.message.game.OperationFaPai";
            vector[1, 13] = "com.guojin.mj.net.message.game.OperationFaPaiRet";
            vector[1, 14] = "com.guojin.mj.net.message.game.OperationOut";
            vector[1, 15] = "com.guojin.mj.net.message.game.OperationOutRet";
            vector[1, 16] = "com.guojin.mj.net.message.game.SyncOpt";
            vector[1, 17] = "com.guojin.mj.net.message.game.SyncOptTime";
            vector[1, 18] = "com.guojin.mj.net.message.game.TingPai";
            vector[1, 19] = "com.guojin.mj.net.message.game.UserOffline";
            vector[1, 20] = "com.guojin.mj.net.message.game.UserPlaceMsg";
            vector[1, 21] = "com.guojin.mj.net.message.game.VoteDelSelect";
            vector[1, 22] = "com.guojin.mj.net.message.game.VoteDelSelectRet";
            vector[1, 23] = "com.guojin.mj.net.message.game.VoteDelStart";
            vector[1, 24] = "com.guojin.mj.net.message.game.PlayerSendChatInfo";
            vector[1, 25] = "com.guojin.mj.net.message.game.PlayerReceiveChatInfo";
            vector[1, 26] = "com.guojin.mj.net.message.game.StaticsResultRet";
            vector[1, 27] = "com.guojin.mj.net.message.game.ServerError";
            vector[1, 28] = "com.guojin.mj.net.message.game.OperationDingPao";
            vector[1, 29] = "com.guojin.mj.net.message.game.OperationDingPaoRet";
            vector[1, 30] = "com.guojin.mj.net.message.game.ShowPaoRet";
            vector[1, 31] = "com.guojin.mj.net.message.game.AllDingPao";
            vector[1, 32] = "com.guojin.mj.net.message.game.ReadyType";
            vector[1, 33] = "com.guojin.mj.net.message.game.StartGameFW";
            vector[1, 34] = "com.guojin.mj.net.message.game.JoinRoomReady";
            vector[1, 35] = "ReqLocation";
            vector[1, 36] = "com.guojin.mj.net.message.game.GameJoinRoomGuShi";
            vector[1, 37] = "com.guojin.mj.net.message.game.GameRoomInfoGuShi";
            vector[1, 38] = "com.guojin.mj.net.message.game.Busying";
            vector[1, 39] = "com.guojin.mj.net.message.game.ShowSelectQueRet";
            vector[1, 40] = "com.guojin.mj.net.message.game.AllQuePaiTypedRet";
            vector[1, 41] = "com.guojin.mj.net.message.game.SelectQuePaiType";
            vector[1, 42] = "com.guojin.mj.net.message.game.SelectQuePaiTypedRet";
            vector[1, 43] = "com.guojin.mj.net.message.game.ShowSelectZuiTypeRet";
            vector[1, 44] = "com.guojin.mj.net.message.game.SelectZuiType";
            vector[1, 45] = "com.guojin.mj.net.message.game.SelectZuiTypeRet";
            vector[1, 46] = "com.guojin.mj.net.message.game.AllDingZuiZi";
            vector[1, 47] = "com.guojin.mj.net.message.game.RecordDetails";
            vector[1, 48] = "com.guojin.mj.net.message.game.RecordDetailsRet";
            vector[1, 49] = "com.guojin.mj.net.message.game.ZhanJiZui";


            vector[3, 0] = "com.guojin.mj.net.message.flower.CreateRoomZJH";//炸金花创建房间信息  发送
            vector[3, 1] = "com.guojin.mj.net.message.flower.CreateRoomRetZJH";//炸金花创建房间信息  返回
            vector[3, 2] = "com.guojin.mj.net.message.flower.OptionEntryZJH";//炸金花房间配置信息
            vector[3, 3] = "com.guojin.mj.net.message.flower.JoinRoomInfoZJH";//炸金花加入房间信息  发送
            vector[3, 4] = "com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH";//炸金花加入房间信息  返回
            vector[3, 5] = "com.guojin.mj.net.message.flower.ReadyRetZJH";//炸金花进入游戏，准备就绪  发送
            vector[3, 6] = "com.guojin.mj.net.message.flower.GameRoomInfoZJH";//炸金花同步房间信息,包含用户信息  返回
            vector[3, 7] = "com.guojin.mj.net.message.flower.GameStartInfoZJH";//炸金花开始游戏信息  发送
            vector[3, 8] = "com.guojin.mj.net.message.flower.FaPaiInfoRetZJH";//炸金花发牌消息  返回
            vector[3, 9] = "com.guojin.mj.net.message.flower.KouDiFenInfoRetZJH";//炸金花扣底分消息  返回
            vector[3, 10] = "com.guojin.mj.net.message.flower.LookCardInfoZJH";//炸金花看牌信息，发送
            vector[3, 11] = "com.guojin.mj.net.message.flower.LookCardInfoRetZJH";//炸金花看牌信息  返回
            vector[3, 12] = "com.guojin.mj.net.message.flower.FollowMoneyInfoZJH";//炸金花跟注信息  发送
            vector[3, 13] = "com.guojin.mj.net.message.flower.FollowMoneyInfoRetZJH";//炸金花跟注结果 返回
            vector[3, 14] = "com.guojin.mj.net.message.flower.KouFenInfoRetZJH";//炸金花扣除分数 返回
            vector[3, 15] = "com.guojin.mj.net.message.flower.CompareCardInfoZJH";//炸金花比牌信息，发送
            vector[3, 16] = "com.guojin.mj.net.message.flower.CompareCardInfoRetZJH";//炸金花比牌结果，返回
            vector[3, 17] = "com.guojin.mj.net.message.flower.PresentGameOverInfoZJH";//炸金花当前局结束，发送
            vector[3, 18] = "com.guojin.mj.net.message.flower.PresentGameOverInfoRetZJH";//炸金花当前局结果，返回
            vector[3, 19] = "com.guojin.mj.net.message.flower.GameOverInfoZJH";//炸金花房间局数完，发送
            vector[3, 20] = "com.guojin.mj.net.message.flower.GameOverInfoRetZJH";//炸金花总局数结果，返回
            vector[3, 21] = "com.guojin.mj.net.message.flower.OuitRoomInfoRetZJH";//炸金花退出房间结果，返回
            vector[3, 22] = "com.guojin.mj.net.message.flower.ClearRoomInfoRetZJH";//炸金花删除房间结果，返回
            vector[3, 23] = "com.guojin.mj.net.message.flower.ClearRoomInfoRetZJH";//炸金花扣除房主房卡，返回
            vector[3, 24] = "com.guojin.mj.net.message.flower.DissolveRoomInfoZJH";//炸金花发起解散房间，发送
            vector[3, 25] = "com.guojin.mj.net.message.flower.DissolveRoomInfoRetZJH";//炸金花解散房间，返回
            vector[3, 26] = "com.guojin.mj.net.message.flower.VoteInfoZJH";//炸金花投票信息，发送
            vector[3, 27] = "com.guojin.mj.net.message.flower.VoteInfoRetZJH";//炸金花投票结果，返回
            vector[3, 28] = "com.guojin.mj.net.message.flower.RecordInfoZJH";//炸金花战绩查询，发送
            vector[3, 29] = "com.guojin.mj.net.message.flower.RecordInfoRetZJH";//炸金花战绩信息，返回
            vector[3, 30] = "com.guojin.mj.net.message.flower.GiveUpCardInfoZJH";//炸金花弃牌信息，发送
            vector[3, 31] = "com.guojin.mj.net.message.flower.GiveUpCardInfoRetZJH";//炸金花弃牌结果，返回
            vector[3, 32] = "com.guojin.mj.net.message.flower.GameUserInfoZJH";//炸金花同步玩家信息，返回
            vector[3, 33] = "com.guojin.mj.net.message.flower.PlayerHandleRetZJH";//炸金花玩家操作信息，返回

            vector[4, 0] = "com.guojin.mj.net.message.game.CreatePdkRoom";//跑得快创建房间
            vector[4, 1] = "com.guojin.mj.net.message.game.CreatePdkRoom_Ret";//跑得
        }
        /// <summary>
        /// 斗牛消息
        /// </summary>
        public void initDNMessageFactory()
        {
            //---------------------------------------------------------------------------------斗牛


            vector[5, 0] = "com.guojin.dn.net.message.DouniuCreateRoom";//创建房间请求
            vector[5, 1] = "com.guojin.dn.net.message.DouniuCreateRoomRet";//创建房间响应
            vector[5, 2] = "com.guojin.dn.net.message.DouniuJoinRoom";//加入房间请求
            vector[5, 3] = "com.guojin.dn.net.message.DouniuJoinRoomRet";//加入房间响应
            vector[5, 7] = "com.guojin.dn.net.message.ExitDouniuRoomResult";//退出房间
            vector[5, 8] = "com.guojin.dn.net.message.DouniuStartGame";//有人准备,开始游戏请求
            vector[5, 9] = "com.guojin.dn.net.message.DouniuStartGameRet";//准备后获取服务器响应
            vector[5, 10] = "com.guojin.dn.net.message.DouniuFaPai";//发牌
            vector[5, 11] = "com.guojin.dn.net.message.DouniuFaPaiRet";//发牌结果
            vector[5, 12] = "com.guojin.dn.net.message.DouniuOperation";//发送(押注，开牌，抢庄等)消息
            vector[5, 13] = "com.guojin.dn.net.message.DouniuOperationRet";//返回(押注，开牌，抢庄等)消息
            vector[5, 20] = "com.guojin.dn.net.message.DouniuChapterMsg";//  服务器发送一局斗牛信息 
            vector[5, 22] = "com.guojin.dn.net.message.DouniuGameChapterEnd";//返回的亮牌结果
            vector[5, 23] = "com.guojin.dn.net.message.DouniuExitRoomRet";//解散房间
            vector[5, 24] = "com.guojin.dn.net.message.DouniuGameRoomInfoRet";//服务器响应数据（包含用户信息,牌信息）
            vector[5, 25] = "com.guojin.dn.net.message.DouniuGameUserInfo";// 用户信息
            vector[5, 27] = "com.guojin.dn.net.message.DNUserPlaceMsg";//   20中的成员
            vector[5, 28] = "com.guojin.dn.net.message.DouniuGameRoomInfo";//通知服务器发送房间数据
            vector[5, 32] = "com.guojin.dn.net.message.SyncDouniuTime";       //   20中的成员
            vector[5, 35] = "com.guojin.dn.net.message.QiangZhuangRet";//返回(押注，开牌，抢庄等)消息
            vector[5, 36] = "com.guojin.dn.net.message.DouniuCompareResult";//返回(押注，开牌，抢庄等)消息
            vector[5, 42] = "com.guojin.dn.net.message.DouniuDelRoomRet";//退出房间投票
            vector[5, 44] = "com.guojin.dn.net.message.DouniuUserOffline";//离线
            vector[5, 45] = "com.guojin.dn.net.message.DouniuVoteRet";//解散房间
            vector[5, 46] = "com.guojin.dn.net.message.DouniuBack";//解散房间
            vector[5, 49] = "com.guojin.dn.net.message.DouniuRoomHistoryListRet";//离线
            vector[5, 51] = "com.guojin.dn.net.message.DouniuChatRet";//DouniuChatRet 
            vector[5, 52] = "com.guojin.dn.net.message.DouniuCasiPai";//抢几手牌
            vector[5, 53] = "com.guojin.dn.net.message.DouniuCasiPaiRet";//抢几手牌相应
            //vector[5, 20] = "com.guojin.dn.net.message.DouniuChapterMsg";//fa
            vector[5, 56] = "com.guojin.dn.net.message.RecordChapterInfoRet";//斗牛战绩Json

            //---------------------------------------------------------------------------------斗牛
        }
        public Message getMessage(int type, int id)
		{
            try
            {
                return (Message)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(vector[type, id], false);

            }
            catch (System.Exception)
            {

                Debug.LogError("MessageFactoryImpi Does't has this message:"+"Type=>>"+type+"  Id=>>"+id);
                throw;
            }

            
		}
        public string getMessageString(int type, int id) {
            return this.vector[type, id];
        }

		public string getMessageClass(int type, int id)
        {
            return vector[type, id];
        }
	}
}