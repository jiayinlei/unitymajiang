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
            vector[7, 35] = "com.guojin.mj.net.message.login.Record";//��ʤ��¼
            vector[7, 36] = "com.guojin.mj.net.message.club.ClubInfo";
            vector[7, 37] = "com.guojin.mj.net.message.login.Report";
            vector[7, 38] = "com.guojin.mj.net.message.login.Playback";
            vector[7, 39] = "com.guojin.mj.net.message.login.PlaybackRet";
            vector[7, 40] = "com.guojin.mj.net.message.login.AlreadyRoomID";
            vector[7, 41] = "com.guojin.mj.net.message.login.AckLocation";






            vector[1, 0] = "com.guojin.mj.net.message.game.GameChapterEnd";//�ƾֽ���
            vector[1, 1] = "com.guojin.mj.net.message.game.GameChapterStart";//��ʼ��Ϸ
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


            vector[3, 0] = "com.guojin.mj.net.message.flower.CreateRoomZJH";//ը�𻨴���������Ϣ  ����
            vector[3, 1] = "com.guojin.mj.net.message.flower.CreateRoomRetZJH";//ը�𻨴���������Ϣ  ����
            vector[3, 2] = "com.guojin.mj.net.message.flower.OptionEntryZJH";//ը�𻨷���������Ϣ
            vector[3, 3] = "com.guojin.mj.net.message.flower.JoinRoomInfoZJH";//ը�𻨼��뷿����Ϣ  ����
            vector[3, 4] = "com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH";//ը�𻨼��뷿����Ϣ  ����
            vector[3, 5] = "com.guojin.mj.net.message.flower.ReadyRetZJH";//ը�𻨽�����Ϸ��׼������  ����
            vector[3, 6] = "com.guojin.mj.net.message.flower.GameRoomInfoZJH";//ը��ͬ��������Ϣ,�����û���Ϣ  ����
            vector[3, 7] = "com.guojin.mj.net.message.flower.GameStartInfoZJH";//ը�𻨿�ʼ��Ϸ��Ϣ  ����
            vector[3, 8] = "com.guojin.mj.net.message.flower.FaPaiInfoRetZJH";//ը�𻨷�����Ϣ  ����
            vector[3, 9] = "com.guojin.mj.net.message.flower.KouDiFenInfoRetZJH";//ը�𻨿۵׷���Ϣ  ����
            vector[3, 10] = "com.guojin.mj.net.message.flower.LookCardInfoZJH";//ը�𻨿�����Ϣ������
            vector[3, 11] = "com.guojin.mj.net.message.flower.LookCardInfoRetZJH";//ը�𻨿�����Ϣ  ����
            vector[3, 12] = "com.guojin.mj.net.message.flower.FollowMoneyInfoZJH";//ը�𻨸�ע��Ϣ  ����
            vector[3, 13] = "com.guojin.mj.net.message.flower.FollowMoneyInfoRetZJH";//ը�𻨸�ע��� ����
            vector[3, 14] = "com.guojin.mj.net.message.flower.KouFenInfoRetZJH";//ը�𻨿۳����� ����
            vector[3, 15] = "com.guojin.mj.net.message.flower.CompareCardInfoZJH";//ը�𻨱�����Ϣ������
            vector[3, 16] = "com.guojin.mj.net.message.flower.CompareCardInfoRetZJH";//ը�𻨱��ƽ��������
            vector[3, 17] = "com.guojin.mj.net.message.flower.PresentGameOverInfoZJH";//ը�𻨵�ǰ�ֽ���������
            vector[3, 18] = "com.guojin.mj.net.message.flower.PresentGameOverInfoRetZJH";//ը�𻨵�ǰ�ֽ��������
            vector[3, 19] = "com.guojin.mj.net.message.flower.GameOverInfoZJH";//ը�𻨷�������꣬����
            vector[3, 20] = "com.guojin.mj.net.message.flower.GameOverInfoRetZJH";//ը���ܾ������������
            vector[3, 21] = "com.guojin.mj.net.message.flower.OuitRoomInfoRetZJH";//ը���˳�������������
            vector[3, 22] = "com.guojin.mj.net.message.flower.ClearRoomInfoRetZJH";//ը��ɾ��������������
            vector[3, 23] = "com.guojin.mj.net.message.flower.ClearRoomInfoRetZJH";//ը�𻨿۳���������������
            vector[3, 24] = "com.guojin.mj.net.message.flower.DissolveRoomInfoZJH";//ը�𻨷����ɢ���䣬����
            vector[3, 25] = "com.guojin.mj.net.message.flower.DissolveRoomInfoRetZJH";//ը�𻨽�ɢ���䣬����
            vector[3, 26] = "com.guojin.mj.net.message.flower.VoteInfoZJH";//ը��ͶƱ��Ϣ������
            vector[3, 27] = "com.guojin.mj.net.message.flower.VoteInfoRetZJH";//ը��ͶƱ���������
            vector[3, 28] = "com.guojin.mj.net.message.flower.RecordInfoZJH";//ը��ս����ѯ������
            vector[3, 29] = "com.guojin.mj.net.message.flower.RecordInfoRetZJH";//ը��ս����Ϣ������
            vector[3, 30] = "com.guojin.mj.net.message.flower.GiveUpCardInfoZJH";//ը��������Ϣ������
            vector[3, 31] = "com.guojin.mj.net.message.flower.GiveUpCardInfoRetZJH";//ը�����ƽ��������
            vector[3, 32] = "com.guojin.mj.net.message.flower.GameUserInfoZJH";//ը��ͬ�������Ϣ������
            vector[3, 33] = "com.guojin.mj.net.message.flower.PlayerHandleRetZJH";//ը����Ҳ�����Ϣ������

            vector[4, 0] = "com.guojin.mj.net.message.game.CreatePdkRoom";//�ܵÿ촴������
            vector[4, 1] = "com.guojin.mj.net.message.game.CreatePdkRoom_Ret";//�ܵ�
        }
        /// <summary>
        /// ��ţ��Ϣ
        /// </summary>
        public void initDNMessageFactory()
        {
            //---------------------------------------------------------------------------------��ţ


            vector[5, 0] = "com.guojin.dn.net.message.DouniuCreateRoom";//������������
            vector[5, 1] = "com.guojin.dn.net.message.DouniuCreateRoomRet";//����������Ӧ
            vector[5, 2] = "com.guojin.dn.net.message.DouniuJoinRoom";//���뷿������
            vector[5, 3] = "com.guojin.dn.net.message.DouniuJoinRoomRet";//���뷿����Ӧ
            vector[5, 7] = "com.guojin.dn.net.message.ExitDouniuRoomResult";//�˳�����
            vector[5, 8] = "com.guojin.dn.net.message.DouniuStartGame";//����׼��,��ʼ��Ϸ����
            vector[5, 9] = "com.guojin.dn.net.message.DouniuStartGameRet";//׼�����ȡ��������Ӧ
            vector[5, 10] = "com.guojin.dn.net.message.DouniuFaPai";//����
            vector[5, 11] = "com.guojin.dn.net.message.DouniuFaPaiRet";//���ƽ��
            vector[5, 12] = "com.guojin.dn.net.message.DouniuOperation";//����(Ѻע�����ƣ���ׯ��)��Ϣ
            vector[5, 13] = "com.guojin.dn.net.message.DouniuOperationRet";//����(Ѻע�����ƣ���ׯ��)��Ϣ
            vector[5, 20] = "com.guojin.dn.net.message.DouniuChapterMsg";//  ����������һ�ֶ�ţ��Ϣ 
            vector[5, 22] = "com.guojin.dn.net.message.DouniuGameChapterEnd";//���ص����ƽ��
            vector[5, 23] = "com.guojin.dn.net.message.DouniuExitRoomRet";//��ɢ����
            vector[5, 24] = "com.guojin.dn.net.message.DouniuGameRoomInfoRet";//��������Ӧ���ݣ������û���Ϣ,����Ϣ��
            vector[5, 25] = "com.guojin.dn.net.message.DouniuGameUserInfo";// �û���Ϣ
            vector[5, 27] = "com.guojin.dn.net.message.DNUserPlaceMsg";//   20�еĳ�Ա
            vector[5, 28] = "com.guojin.dn.net.message.DouniuGameRoomInfo";//֪ͨ���������ͷ�������
            vector[5, 32] = "com.guojin.dn.net.message.SyncDouniuTime";       //   20�еĳ�Ա
            vector[5, 35] = "com.guojin.dn.net.message.QiangZhuangRet";//����(Ѻע�����ƣ���ׯ��)��Ϣ
            vector[5, 36] = "com.guojin.dn.net.message.DouniuCompareResult";//����(Ѻע�����ƣ���ׯ��)��Ϣ
            vector[5, 42] = "com.guojin.dn.net.message.DouniuDelRoomRet";//�˳�����ͶƱ
            vector[5, 44] = "com.guojin.dn.net.message.DouniuUserOffline";//����
            vector[5, 45] = "com.guojin.dn.net.message.DouniuVoteRet";//��ɢ����
            vector[5, 46] = "com.guojin.dn.net.message.DouniuBack";//��ɢ����
            vector[5, 49] = "com.guojin.dn.net.message.DouniuRoomHistoryListRet";//����
            vector[5, 51] = "com.guojin.dn.net.message.DouniuChatRet";//DouniuChatRet 
            vector[5, 52] = "com.guojin.dn.net.message.DouniuCasiPai";//��������
            vector[5, 53] = "com.guojin.dn.net.message.DouniuCasiPaiRet";//����������Ӧ
            //vector[5, 20] = "com.guojin.dn.net.message.DouniuChapterMsg";//fa
            vector[5, 56] = "com.guojin.dn.net.message.RecordChapterInfoRet";//��ţս��Json

            //---------------------------------------------------------------------------------��ţ
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