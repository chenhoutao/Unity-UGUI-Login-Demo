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
//Json���ݹ�����
//��Ҫ����Json�����л����浽Ӳ�̣��ͷ����л�����Ӳ���ж�ȡ���ڴ棩
public class JsonMgr : MonoBehaviour
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance=>instance;
    private JsonMgr() { }
    
    public void SaveData(object data, string filename, JsonType type = JsonType.LitJson)
    {
        //ָ���洢·��
        string path = Application.persistentDataPath + "/" + filename+".json";
        //print(path);
        //���л��õ�Json�ַ���
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
        //�����л���Json�ִ��浽ָ��·��
        File.WriteAllText(path,jsonStr);
    }

    public T LoadData<T>(string filename , JsonType type = JsonType.LitJson) where T : new()
    {
        #region ȷ�����Ķ�
        //1.�ж�Ĭ�������Ƿ���ڸ��ļ�
        string path = Application.streamingAssetsPath + "/" + filename + ".json";
        //print(Application.streamingAssetsPath);
        //print(File.Exists(path));
        //2.������ȥ��д�ļ������
        if (!File.Exists(path)) path = Application.persistentDataPath + "/" + filename+ ".json";
        //3.����д�ļ�����û��,�򷵻�Ĭ��ֵ
        if (!File.Exists(path)) return new T();
        #endregion

        #region ���з����л�
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
