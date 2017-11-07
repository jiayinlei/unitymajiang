using System.Collections.Generic;
using com.guojin.core.io;
using UnityEngine;

namespace com.guojin.core.utils
{
    /*
     * 工具类
     */
    public class Utils
    {
        //用于储存从服务器接受到的消息
        public static com.guojin.core.io.message.Message mess;
        public static string id;
        public static string name;
        public static string num;//房间号
        public static com.guojin.mj.net.message.login.SysSetting syssetting;
        public static void AddMusic(GameObject go,AudioClip ac) {
            AudioSource AS = go.AddComponent<AudioSource>();
            AS.clip = ac;
            AS.Play();


        }

        //public com.guojin.core.io.message.Message ReturnMessage(int id)
        //{
            
        //}
        /// <summary>
        /// 发送消息的Demo  以创建房间为例 八局，无马
        /// </summary>
        public static void SendMessage()
        {
            List<com.guojin.mj.net.message.login.OptionEntry> options = new List<mj.net.message.login.OptionEntry>();
            com.guojin.mj.net.message.login.OptionEntry OE = mj.net.message.login.OptionEntry.create("chapterMax","8");
            com.guojin.mj.net.message.login.OptionEntry OE1 = mj.net.message.login.OptionEntry.create("maiMa","0");
            com.guojin.mj.net.message.login.OptionEntry OE2 = mj.net.message.login.OptionEntry.create("bianType", "tuiDaoHu");
            options.Add(OE);
            options.Add(OE1);
            options.Add(OE2);
            com.guojin.mj.net.message.login.CreateRoom cr = mj.net.message.login.CreateRoom.create("bj", options);
            //cr.toString();
            Debug.Log(cr.toString());
            com.guojin.mj.net.Net.instance.write(cr);

        }
        /// <summary>
        /// 处理接收的消息
        /// </summary>
        /// <param name="type"></param>传递的type
        /// <param name="id"></param>传递的id
        /// <param name="message"></param>后边的string
        public static void DisposeMessage()
        {
            com.guojin.mj.net.message.login.Ping ping = com.guojin.mj.net.message.login.Ping.create(mess.toString());
           
            com.guojin.mj.net.Net.instance.write(ping);
        }


          public static int rightMove(int value, int pos)
          {

              if (pos != 0)  //移动 0 位时直接返回原值
              {
                  int mask = 0x7fffffff;     // int.MaxValue = 0x7FFFFFFF 整数最大值
                  value >>= 1;     //第一次做右移，把符号也算上，无符号整数最高位不表示正负
                                            //但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
                  value &= mask;     //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
                  value >>= pos-1;     //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
             }
              return value;
          }

        public static void putThree(Byte _out, int v)
        {
            if (_out.endian == Byte.BIG_ENDIAN)
            {
                _out.writeByte(rightMove(v, 16));
                _out.writeByte(rightMove(v, 8));
                _out.writeByte(rightMove(v, 0));
            }
            else
            {
                _out.writeByte(rightMove(v, 0));
                _out.writeByte(rightMove(v, 8));
                _out.writeByte(rightMove(v, 16));
            }
        }

        public static int getThree(Byte in0)
        {
            int ch1;
            int ch2;
            int ch3;

            if (in0.endian == Byte.BIG_ENDIAN)
            {
                ch1 = in0.getUint8();
                ch2 = in0.getUint8();
                ch3 = in0.getUint8();
                return ((ch1 << 16) + (ch2 << 8) + (ch3 << 0));
            }
            else
            {
                ch1 = in0.getUint8();
                ch2 = in0.getUint8();
                ch3 = in0.getUint8();
                return ((ch1 << 0) + (ch2 << 8) + (ch3 << 16));
            }
        }

        /*
         * 调试List<byte>
         */
        public static string debug(List<byte> list)
        {
            return System.String.Join(",", toHexArray(System.Array.ConvertAll(list.ToArray(), (System.Converter<byte, int>) System.Convert.ToInt32)));
        }

        /* int数组转hex数组 */
        public static string[] toHexArray (int[] intArr)
        {
            string[] hexArr= new string[intArr.Length];
            for(int i=0; i < intArr.Length; i++) {
                hexArr[i] = System.String.Format("{0:X2}", intArr[i]);
            }

            return hexArr;
        }

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>
        /// <returns></returns>
        public static long GetTimeStamp(bool bflag = true)
        {
            System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            long ret;
            if (bflag)
                ret = System.Convert.ToInt64(ts.TotalSeconds);
            else
                ret = System.Convert.ToInt64(ts.TotalMilliseconds);
            return ret;
        }
    }
}