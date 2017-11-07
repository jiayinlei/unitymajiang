using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using com.guojin.mj.net.message.login;
using UnityEngine.SceneManagement;
using System;
using LitJson;

public class PlaybackLayer : Observer
{
    /*public static PlaybackLayer instance = new PlaybackLayer();
    public static PlaybackLayer GetInstance()
    {
        return PlaybackLayer.instance;
    }*/
    protected override string[] GetMsgList()
    {
        return new string[] {
             MessageFactoryImpi.instance.getMessageString(1,7),
              MessageFactoryImpi.instance.getMessageString(1,37),
             MessageFactoryImpi.instance.getMessageString(7, 39),
        };
    }
    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback && msg == MessageFactoryImpi.instance.getMessageString(1, 7))
        {
            com.guojin.mj.net.message.game.GameRoomInfo GJM = (com.guojin.mj.net.message.game.GameRoomInfo)data;
            Method.GRI = GJM;
            Method.MJType = "MaJongZhengZhou";
            Method.isCreater = GJM.sceneUser[0].userId == GameData.GetInstance().playerData.id ? true : false;
            //Method.isCreater = true;
            Method.maJongJuNum = GJM.leftChapterNums.ToString();
            Debug.Log("UIState_LoadingPage--MJCreatRoomPageEvent--ContinueBtn");
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        }
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback && msg == MessageFactoryImpi.instance.getMessageString(1, 37))
        {
            com.guojin.mj.net.message.game.GameRoomInfoGuShi GJM = (com.guojin.mj.net.message.game.GameRoomInfoGuShi)data;
            Method.GRIGuShi = GJM;
            Method.MJType = "MaJongGuShi";
            Method.isCreater = GJM.sceneUser[0].userId == GameData.GetInstance().playerData.id ? true : false;
            //Method.isCreater = true;
            Method.maJongJuNum = GJM.leftChapterNums.ToString();
            Debug.Log("UIState_LoadingPage--PlaybackLayer");
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 39))
        {
            PlaybackRet playbackRet = (PlaybackRet)data;
            if (playbackRet.recordDetail.Length > 0x80)
            {
                inPlayback = true;
                Method.isPlayerBack = true;
                Playback(playbackRet.recordDetail);
            }

        }
    }

    private void Start()
    {
        this.initMsg();
    }
   
    public void Playback(string playbackDataStr)
    {
        if (playbackDataStr.Equals(""))
        {
            playbackDataStr = "{\"joinGame\":\"052907f41a41e59bbde98791e8bdafe4bbb6e680bbe7bb8fe79086f444687474703a2f2f3132322e3131342e3135302e33353a383038302f69636f6e2f6f7158576e776e5435754759766338514973656158366453634333592e6a7067050af9fffffff204fa018958fdf4113231392e3135352e32322e3536fef40f3131312e31e585ace9878cf40f3334362e38e585ace9878cfefdf40e3131332e363336373435f40c33342e3833303131f40de5bca0e4b8bde88bb9f444687474703a2f2f3132322e3131342e3135302e33353a383038302f69636f6e2f6f7158576e7773487344425178696d4b56756c655f43775a42426d492e6a70670419f9fffffff705fa018950fdf4123232332e38382e3132382e323439f40f3131312e31e585ace9878cfef40f3339392e35e585ace9878cfefdf40e3131322e343530303237f40d33342e363132383839f408efbc8c4cf444687474703a2f2f3132322e3131342e3135302e33353a383038302f69636f6e2f6f7158576e7770765a3175335970396831355f7054684732592d4c452e6a706704041b06fa018952fdf4113232332e39302e37392e313430f40f3334362e38e585ace9878cf40f3339392e35e585ace9878cfefefdf40e3131352e363331303237f40d33322e313931373235fdf40a35323833313605fa018958fd461203fe030712040b0a0c100e14120f0d181d1e1c0404040404040405040706050604050503f40538120a07060609051c1a171b130f150e04040404040404040404040403f4063130120b080c040b150f191616161a1c1a0404040404040407040704050405050403f405390607080406fefefefd06f9ffff76fcfefefe080704fefe04f404fefe05fe0403\",\"queList\":[{\"queTypeRet\":\"052e040405\",\"index\":1},{\"queTypeRet\":\"052e050f06\",\"index\":2},{\"queTypeRet\":\"052e061506\",\"index\":3}],\"gameChapterEnd\":\"050404050403040718070e101c040f0e18121c1010111d1e0f180304050d040404f40ae782b9e782aef40ae782b9e782ae04f41a41e59bbde98791e8bdafe4bbb6e680bbe7bb8fe790860504030303081a1b1d1d030404040706170904fefe04f40de5bca0e4b8bde88bb902030303030e191616161a041a0419190304050b040404fefe04f408efbc8c4c0505\",\"userOperates\":[{\"opt\":\"FA\",\"operate\":\"0514f406464106040306\",\"index\":1,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554060f050f06\",\"index\":2,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b060f050f06\",\"index\":3,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104070304\",\"index\":4,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554040a050a04\",\"index\":5,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b040a050a04\",\"index\":6,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641050d0305\",\"index\":7,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540513051305\",\"index\":8,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0513051305\",\"index\":9,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106190306\",\"index\":10,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540608050806\",\"index\":11,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0608050806\",\"index\":12,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104110304\",\"index\":13,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540407050704\",\"index\":14,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0407050704\",\"index\":15,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641051b0305\",\"index\":16,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540515051505\",\"index\":17,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0515051505\",\"index\":18,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f4064641060e0306\",\"index\":19,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554060e050e06\",\"index\":20,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b060e050e06\",\"index\":21,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104100304\",\"index\":22,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554040b050b04\",\"index\":23,\"locationIndex\":0},{\"opt\":\"PENG\",\"operate\":\"0514f40850454e47060b050b04\",\"index\":24,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554060c050c06\",\"index\":25,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b060c050c06\",\"index\":26,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f4064641040d0304\",\"index\":27,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554040c050c04\",\"index\":28,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b040c050c04\",\"index\":29,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641051d0305\",\"index\":30,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554050e050e05\",\"index\":31,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b050e050e05\",\"index\":32,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f4064641060a0306\",\"index\":33,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554060a050a06\",\"index\":34,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b060a050a06\",\"index\":35,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f4064641040a0304\",\"index\":36,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554040a050a04\",\"index\":37,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b040a050a04\",\"index\":38,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641051e0305\",\"index\":39,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554050d050d05\",\"index\":40,\"locationIndex\":1},{\"opt\":\"PENG\",\"operate\":\"0514f40850454e47040d050d05\",\"index\":41,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540414051404\",\"index\":42,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0414051404\",\"index\":43,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105170305\",\"index\":44,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554051b051b05\",\"index\":45,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b051b051b05\",\"index\":46,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106130306\",\"index\":47,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540613051306\",\"index\":48,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0613051306\",\"index\":49,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104060304\",\"index\":50,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540406050604\",\"index\":51,\"locationIndex\":0},{\"opt\":\"PENG\",\"operate\":\"0514f40850454e470506050604\",\"index\":52,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540505050505\",\"index\":53,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0505050505\",\"index\":54,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f4064641060b0306\",\"index\":55,\"locationIndex\":2},{\"opt\":\"XIAO_MING_GANG\",\"operate\":\"0514f4125849414f5f4d494e475f47414e47060b050b04\",\"index\":56,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464106090306\",\"index\":57,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540609050906\",\"index\":58,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0609050906\",\"index\":59,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104080304\",\"index\":60,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540408050804\",\"index\":61,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0408050804\",\"index\":62,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105050305\",\"index\":63,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540507050705\",\"index\":64,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0507050705\",\"index\":65,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106140306\",\"index\":66,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540614051406\",\"index\":67,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0614051406\",\"index\":68,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104100304\",\"index\":69,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540410051004\",\"index\":70,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0410051004\",\"index\":71,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105140305\",\"index\":72,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540514051405\",\"index\":73,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0514051405\",\"index\":74,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f4064641060c0306\",\"index\":75,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554060c050c06\",\"index\":76,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b060c050c06\",\"index\":77,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104110304\",\"index\":78,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540411051104\",\"index\":79,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0411051104\",\"index\":80,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105120305\",\"index\":81,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540512051205\",\"index\":82,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0512051205\",\"index\":83,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106080306\",\"index\":84,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540608050806\",\"index\":85,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0608050806\",\"index\":86,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f4064641041d0304\",\"index\":87,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554041d051d04\",\"index\":88,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b041d051d04\",\"index\":89,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105180305\",\"index\":90,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554051c051c05\",\"index\":91,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b051c051c05\",\"index\":92,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106190306\",\"index\":93,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554061c051c06\",\"index\":94,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b061c051c06\",\"index\":95,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104170304\",\"index\":96,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540417051704\",\"index\":97,\"locationIndex\":0},{\"opt\":\"PENG\",\"operate\":\"0514f40850454e470517051704\",\"index\":98,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540505050505\",\"index\":99,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0505050505\",\"index\":100,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106150306\",\"index\":101,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540615051506\",\"index\":102,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0615051506\",\"index\":103,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104050304\",\"index\":104,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540405050504\",\"index\":105,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0405050504\",\"index\":106,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105090305\",\"index\":107,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554050a050a05\",\"index\":108,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b050a050a05\",\"index\":109,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106120306\",\"index\":110,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540612051206\",\"index\":111,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0612051206\",\"index\":112,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f4064641040d0304\",\"index\":113,\"locationIndex\":0},{\"opt\":\"XIAO_MING_GANG\",\"operate\":\"0514f4125849414f5f4d494e475f47414e47040d050d06\",\"index\":114,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641041e0304\",\"index\":115,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554041e051e04\",\"index\":116,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b041e051e04\",\"index\":117,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105130305\",\"index\":118,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540513051305\",\"index\":119,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0513051305\",\"index\":120,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106080306\",\"index\":121,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540608050806\",\"index\":122,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0608050806\",\"index\":123,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f4064641040f0304\",\"index\":124,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554040f050f04\",\"index\":125,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b040f050f04\",\"index\":126,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f406464105150305\",\"index\":127,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540515051505\",\"index\":128,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0515051505\",\"index\":129,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106100306\",\"index\":130,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540610051006\",\"index\":131,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0610051006\",\"index\":132,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f406464104060304\",\"index\":133,\"locationIndex\":0},{\"opt\":\"GUO\",\"operate\":\"0514f40747554f04030303\",\"index\":134,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540406050604\",\"index\":135,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0406050604\",\"index\":136,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641051e0305\",\"index\":137,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554051e051e05\",\"index\":138,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b051e051e05\",\"index\":139,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106130306\",\"index\":140,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540613051306\",\"index\":141,\"locationIndex\":2},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b0613051306\",\"index\":142,\"locationIndex\":2},{\"opt\":\"FA\",\"operate\":\"0514f4064641041b0304\",\"index\":143,\"locationIndex\":0},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554041b051b04\",\"index\":144,\"locationIndex\":0},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b041b051b04\",\"index\":145,\"locationIndex\":0},{\"opt\":\"FA\",\"operate\":\"0514f4064641051d0305\",\"index\":146,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f5554051e051e05\",\"index\":147,\"locationIndex\":1},{\"opt\":\"OUT_OK\",\"operate\":\"0514f40a4f55545f4f4b051e051e05\",\"index\":148,\"locationIndex\":1},{\"opt\":\"FA\",\"operate\":\"0514f406464106090306\",\"index\":149,\"locationIndex\":2},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540609050906\",\"index\":150,\"locationIndex\":2},{\"opt\":\"PENG\",\"operate\":\"0514f40850454e470509050906\",\"index\":151,\"locationIndex\":1},{\"opt\":\"OUT\",\"operate\":\"0514f4074f55540518051805\",\"index\":152,\"locationIndex\":1}]}";
        }  

        playbackModel = JsonMapper.ToObject<PlaybackModel>(playbackDataStr);
        JsonUtility.FromJson<PlaybackModel>(playbackDataStr);

        Debug.Log("playbackTest");
        Debug.Log(playbackModel.joinGame);
        executeIntrust(playbackModel.joinGame);
        //StartCoroutine("Timer");

    }
  
    public void Playback1()
    {
        inPlayback = true;
        Method.isPlayerBack = true;
        Playback("");
    }
    public void executeIntrust(string intrust)
    {
        byte[] instuctBytes = hexStringToByte(intrust);

        //com.guojin.core.io.Byte readBuffer = new com.guojin.core.io.Byte(new List<byte>(instuctBytes));
        //readBuffer.endian = com.guojin.core.io.Byte.BIG_ENDIAN;
        //readBuffer.pos = 0;
        //com.guojin.core.io.MarkCompressInput in0 = com.guojin.core.io.MarkCompressInput.create(readBuffer);
        //int type = in0.readInt();
        //int id = in0.readInt();
        //com.guojin.core.io.message.Message msg = com.guojin.mj.net.message.MessageFactoryImpi.instance.getMessage(type, id);
        //msg.decode(in0);
        //if (type == 1 && id == 16)
        //{
        //    com.guojin.mj.net.message.game.SyncOpt so = (com.guojin.mj.net.message.game.SyncOpt)msg;
        //}

        SocketMgr.GetInstance().OnWebSocketUnityReceiveData(instuctBytes);
        
    }

    public static byte[] hexStringToByte(string hex)
    {
        int len = (hex.Length / 2);
        byte[] result = new byte[len];
        char[] achar = hex.ToCharArray();
        for (int i = 0; i < len; i++)
        {
            int pos = i * 2;
            result[i] = (byte)(toByte(achar[pos]) << 4 | toByte(achar[pos + 1]));
        }
        return result;
    }
    private static byte toByte(char c)
    {
        c = Char.ToUpper(c);
        byte b = (byte)"0123456789ABCDEF".IndexOf(c);
        return b;
    }
    public void startTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(getPlaybackSpead() / 1000f);
            if (!playbackTimer()) break;
           
        }
    }
    //回放功能
    public bool inPlayback = false;
	public PlaybackModel playbackModel;
	public int instructIndex = 0;
    public int queTypeindex = 0;
	public bool playbackPause = false;
	//回放速度，0,1,2三挡
	public int playbackSpeed = 1;

    public bool playbackTimer()
	{
        if (!inPlayback) return false;
        if (playbackPause) return true;
        if (playbackModel.queList!=null)
        {
            if (playbackModel != null && playbackModel.queList.Count > queTypeindex)
            {
                executeIntrust(playbackModel.queList[queTypeindex].queTypeRet);
                queTypeindex++;
            }
            else
            {
                if (playbackModel != null && playbackModel.userOperates.Count > instructIndex)
                {

                    if (playbackModel.userOperates[instructIndex].opt == "OUT_OK")
                    {
                        instructIndex++;
                        if (playbackModel.userOperates.Count <= instructIndex) return true;
                    }
                    executeIntrust(playbackModel.userOperates[instructIndex].operate);
                    instructIndex++;
                }
                else
                {
                    executeIntrust(playbackModel.gameChapterEnd);
                    //Laya.timer.clear(this, playbackTimer);
                    //ChapterResultDialog.dialog.confirmBtn.on(Event.CLICK, this, function(){ UiManager.instance.goMain(); });
                    //ChapterResultDialog.dialog.closeBtn.on(Event.CLICK, this, function(){ UiManager.instance.goMain(); });
                    //resetPlayback();
                    return false;
                }
            }
           
        }
        else
        {
            if (playbackModel != null && playbackModel.userOperates.Count > instructIndex)
            {

                if (playbackModel.userOperates[instructIndex].opt == "OUT_OK")
                {
                    instructIndex++;
                    if (playbackModel.userOperates.Count <= instructIndex) return true;
                }
                executeIntrust(playbackModel.userOperates[instructIndex].operate);
                instructIndex++;
            }
            else
            {
                executeIntrust(playbackModel.gameChapterEnd);
                //Laya.timer.clear(this, playbackTimer);
                //ChapterResultDialog.dialog.confirmBtn.on(Event.CLICK, this, function(){ UiManager.instance.goMain(); });
                //ChapterResultDialog.dialog.closeBtn.on(Event.CLICK, this, function(){ UiManager.instance.goMain(); });
                //resetPlayback();
                return false;
            }
        }	
        return true;
	}
		
	public int getPlaybackSpead()
	{
		//if (playbackSpeed == 0) {
		//	return 2000;
		//} else if (playbackSpeed == 2) {
		//	return 750;
		//} else {
		//	return 1200;
		//}
        if(playbackSpeed == 0) {
            return 1000;
        } else if (playbackSpeed == 2)
        {
            return 250;
        }
        else if (playbackSpeed == 3)
        {
            return 100;
        }
        else
        {
            return 500;
        }
    }
		
	public void pauseOrResumePlayback()
	{
		if (playbackPause) {
            GameObject.Find("pauseBtn").GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/hist_rec_pause");
            playbackPause = false;
        }
        else {
            GameObject.Find("pauseBtn").GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/hist_rec_play");
            playbackPause = true;
        }
    }

    public void forwardPlayback()
	{
		if (playbackSpeed< 3) {
			playbackSpeed++;
            GameObject.Find("dangweiT").GetComponent<Text>().text = "X " + (playbackSpeed + 1);
        }
    }

    public void backwardPlayback()
	{
		if (playbackSpeed > 0) {
			playbackSpeed--;
            GameObject.Find("dangweiT").GetComponent<Text>().text = "X " + (playbackSpeed + 1);
        }
    }

    public void exitPlayback()
	{
        //resetPlayback();

        Method.LoadMainCity();
       //回放界面，返回

    }

    public void resetPlayback()
	{
        Method.isPlayerBack = false;
        inPlayback = false;
		playbackModel = null;
		instructIndex = 0;
		playbackPause = false;
		playbackSpeed = 1;
        queTypeindex = 0;

    }

    public List<int>[] HandCard = new List<int>[4];
    public void ShowHandCard(int index)
    {
        List<int> handCard = HandCard[index];
        handCard.Sort();
        Transform handCardArea = GameObject.Find("MahjongContent" + index).transform;
        for (int i = 0; i < handCardArea.childCount; i++)
        {
            Destroy(handCardArea.GetChild(i).gameObject);
        }

        for (int i = handCard.Count - 1; i >= 0; i--)
        {
            GameObject temp = Instantiate(Method.clon);
            temp.GetComponentInChildren<MJInfo>().CreatMJInfo(handCard[i], index, Method.MjNameSp[handCard[i]]);
            temp.transform.SetParent(handCardArea);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            if (index == 1)
            {
                temp.transform.Rotate(0, 0, 90);
                temp.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            }
            if (index == 2)
            {
                temp.transform.Rotate(0, 0, 180);
                temp.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }
            if (index == 3)
            {
                temp.transform.Rotate(0, 0, -90);
                temp.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            }
        }

    }
}
public class PlaybackModel
{
    public string joinGame;
    public List<queType> queList;
    public string gameChapterEnd;   
    public List<PlaybackInstruct> userOperates;
}
public class PlaybackInstruct
{
    public string opt;
    public string operate;
    public int index;
    public int locationIndex;
}
public class queType
{
    public string queTypeRet;
    public int index;
}

