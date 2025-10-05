using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public enum JsonType
{
    JsonUtlity,
    LitJson,
}
//Json数据管理类
//主要用于Json的序列化（存到硬盘）和反序列化（从硬盘中读取到内存）
public class JsonMgr : MonoBehaviour
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance=>instance;
    private JsonMgr() { }
    
    public void SaveData(object data, string filename, JsonType type = JsonType.LitJson)
    {
        //指定存储路径
        string path = Application.persistentDataPath + "/" + filename+".json";
        //print(path);
        //序列化得到Json字符串
        string jsonStr = "";
        switch(type)
        {
            case JsonType.JsonUtlity:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }
        //存序列化的Json字串存到指定路径
        File.WriteAllText(path,jsonStr);
    }

    public T LoadData<T>(string filename , JsonType type = JsonType.LitJson) where T : new()
    {
        #region 确定从哪读
        //1.判断默认数据是否存在该文件
        string path = Application.streamingAssetsPath + "/" + filename + ".json";
        //print(Application.streamingAssetsPath);
        //print(File.Exists(path));
        //2.若无则去读写文件夹里读
        if (!File.Exists(path)) path = Application.persistentDataPath + "/" + filename+ ".json";
        //3.若读写文件夹里没有,则返回默认值
        if (!File.Exists(path)) return new T();
        #endregion

        #region 进行反序列化
        string jsonStr = File.ReadAllText(path);
        T data = default(T);
        
        switch (type)
        {
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }
        return data;
        #endregion


    }

}
