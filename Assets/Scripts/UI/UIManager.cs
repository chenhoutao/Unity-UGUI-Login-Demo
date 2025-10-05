using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance=> instance;
    private Dictionary<string,BasePanel> panelDic= new Dictionary<string,BasePanel>();//�浱ǰ��ʾ����������
    private Transform canvasTransform;
    private UIManager()
    {
        canvasTransform = GameObject.Find("Canvas").transform;//�õ������ϴ����õ�canvas
        GameObject.DontDestroyOnLoad(canvasTransform.gameObject);//��canvas������������Ƴ�
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName=typeof(T).Name;
        if (panelDic.ContainsKey(panelName) ) return panelDic[panelName] as T;//������������ʾ���������ٴ����ˣ�ֱ�ӷ���
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));//��������panelName��̬�������
        panelObj.transform.SetParent(canvasTransform, false);//�Ѹ�������canvas��
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);//�����ӵ���ǰ��ʾ���������
        panel.ShowMe();//��ʼ���룬alpha��0��1
        return panel;
        
    }
    public void HidePanel<T>(bool isFade = true)where T : BasePanel//������true��ֱ�����ش�false
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))//�жϵ�ǰ��ʾ������У����޸����
        {
            if(isFade)
            {
                panelDic[panelName].HideMe(() => 
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                }
                );
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    public T GetPanel<T>() where T :BasePanel
    {
        string panelName = typeof(T).Name;
        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
}
