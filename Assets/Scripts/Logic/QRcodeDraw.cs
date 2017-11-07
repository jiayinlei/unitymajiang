
using UnityEngine;
using System.Collections;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class QRcodeDraw : EventManager
{
    private Texture2D encoded;
    //指定字符串
    /*
    private string QRCodes = "http://gsqyjcs.guojinkj.com:8080/oauth2/qyj?roomNo=123456&pid=100525";
    */
    private string QRCodes = "http://gsqyjcs.guojinkj.com:8080/oauth2/qyj?";
    public RawImage QRImage;

    void Start()
    {
        ShowCode();
    }
    void FrendShare()
    {

    }
    void LineShare()
    {

    }
    //定义方法生成二维码
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }



    public void ShowCode()
    {
        encoded = new Texture2D(256, 256);
        var textForEncoding = QRCodes;
        if (textForEncoding != null)
        {
            //二维码写入图片
            var color32 = Encode(textForEncoding, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            //重新赋值一张图，计算大小,避免白色边框过大
            Texture2D encoded1;
            encoded1 = new Texture2D(190, 190);//创建目标图片大小
            encoded1.SetPixels(encoded.GetPixels(32, 32, 190, 190));
            encoded1.Apply();
            QRImage.texture = encoded;
        }
    }

    public override void InformationSetting()
    {
        throw new System.NotImplementedException();
    }
    void CloseBtnClick()
    {
        DestroyImmediate(this.gameObject);
    }


}