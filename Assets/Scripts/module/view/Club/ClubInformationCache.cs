using System;
using com.guojin.core.utils;

/* ***********************************************
 * Describe:俱乐部信息缓存
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/15 13:22:27 
 * FileName: ClubInformationCache 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.mj.net.message.club {
    class ClubInformationCache: Singleton<ClubInformationCache>{

        private bool hasInClub = false;
        private String clubId;   //俱乐部Id
        private String createUserName;//创建者真实姓名
        private String wxNo;    // 微信号
        private String notice;    //公告

        public string ClubId {
            get {
                return clubId;
            }

            set {
                clubId = value;
            }
        }

        public string CreateUserName {
            get {
                return createUserName;
            }

            set {
                createUserName = value;
            }
        }

        public string WxNo {
            get {
                return wxNo;
            }

            set {
                wxNo = value;
            }
        }

        public string Notice {
            get {
                return notice;
            }

            set {
                notice = value;
            }
        }

        public bool HasInClub {
            get {
                return hasInClub;
            }

            set {
                hasInClub = value;
            }
        }
    }
}
