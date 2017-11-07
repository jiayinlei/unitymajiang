using UnityEngine;
using System.Xml;
using System.IO;
using LitJson;
public class ReadFile : MonoBehaviour
{
    public static string[] value;
    public static string jsonInfo;
    public  static string LoadJsonFromFile(string path)
    {
        string fileName = Application.dataPath + path;
        if (File.Exists(fileName))
        {
            StreamReader sr = new StreamReader(fileName);
            if (sr != null)
            {
              jsonInfo = sr.ReadToEnd();
            }
        }
        return jsonInfo;
    }
    public static string[] ReadJson(string json)
    {
        JsonData date = JsonMapper.ToObject(json);
         value = new string[date.Count];
        for (int i = 0; i < date.Count; i++)
        {
            value[i]= date[i]["word"].ToString();
        }
        return value;
    
    }

    #region
    /*
	//读取XML文件的方法
    public void LoadPhraseXml(string path,ArrayList Phrase)
    {
        Dictionary<int, string> ReadFile = new Dictionary<int, string>();
        XmlDocument PhraseXml = new XmlDocument();
        PhraseXml.Load(path);
        XmlNode xn = PhraseXml.SelectSingleNode("Phrase");
        XmlNodeList xnl = xn.ChildNodes;
        foreach (XmlNode xmlnode in xnl)
        {
            XmlElement xe = xmlnode as XmlElement;
            int num = int.Parse(xe.Attributes["num"].Value);
            string info = xe.Attributes["info"].Value;
            ReadFile.Add(num,info);
        }
    }
    */
    #endregion

  
   
  
}



