/* ============================================================================
** Author	: 1130282072@qq.com
** Comments	: 常量定义
** ----------------------------------------------------------------------------
** History	:	DATE			DESC                    
**				2017-7-2		Create by sunfei         
** ============================================================================
*/

//名称的常量
public class UiNameConst
{
 
}
public class DefaultKeyConst
{
    public const string VisitorTokenKey = "VisitorTokenKey";
    public const string version = "1.0.5";
    

}
//存储路径的常量
public class PathConst
{

}
//数值常量
public class NumConst
{
    public const string IosTelNum = "15378733044";
}
// URL 常量
public class URLConst
{
    public const string DefaultUserHeadUrl = "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0";
#if CS    //测试服务器
    public const string WXAppLoginCallBackUrl = "http://122.114.150.35:8080/appWeixinLogin.json";
    public const string ServerAdress = "ws://122.114.150.35:8010/g";
    public const string VersionURL = "http://122.114.150.35:8080/version";
#elif ZS //正式服务器
    public const string WXAppLoginCallBackUrl = "http://gjmjceshi.guojin123.net:8070/appWeixinLogin.json";
    public const string ServerAdress = "ws://gjmjceshi.guojin123.net:8026/g";
    public const string VersionURL = "http://gjmjceshi.guojin123.net:8070/version";
#elif WX //王曦服务器
    public const string WXAppLoginCallBackUrl = "http://122.114.150.35:8080/appWeixinLogin.json";
    public const string ServerAdress = "ws://192.168.1.82:8020/g";
    public const string VersionURL = "http://122.114.150.35:8080/version";
#elif YFB //yang服务器
    public const string WXAppLoginCallBackUrl = "http://192.168.1.86:8080/appWeixinLogin.json";
    public const string ServerAdress = "ws://192.168.1.86:8010/g";
    public const string VersionURL = "http://122.114.150.35:8080/version";
#elif QXH //qi服务器
    public const string WXAppLoginCallBackUrl = "http://192.168.1.83:8080/appWeixinLogin.json";
    public const string ServerAdress = "ws://192.168.1.83:8020/g";
    public const string VersionURL = "http://122.114.150.35:8080/version";
#elif QXH2 //qi服务器
    public const string WXAppLoginCallBackUrl = "http://192.168.1.83:8080/appWeixinLogin.json";
    public const string ServerAdress = "ws://192.168.1.83:8010/g";
    public const string VersionURL = "http://122.114.150.35:8080/version";
#elif LY //刘艺辉服务器
    public const string WXAppLoginCallBackUrl = "http://192.168.1.62:8080/appWeixinLogin.json";
    public const string ServerAdress = "ws://192.168.1.62:8010/g";
    public const string VersionURL = "http://122.114.150.35:8080/version";
#endif
    public const string IosDailiURL = "http://majiang1.guojin123.net";
    public const string IosShareURL = "http://gjmjceshi.guojin123.net:8070/share.html";
}