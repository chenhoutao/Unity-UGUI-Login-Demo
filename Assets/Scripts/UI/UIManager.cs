using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance=> instance;
    private Dictionary<string,BasePanel> panelDic= new Dictionary<string,BasePanel>();//存当前显示的面板的容器
    private Transform canvasTransform;
    private UIManager()
    {
        canvasTransform = GameObject.Find("Canvas").transform;//得到场景上创建好的canvas
        GameObject.DontDestroyOnLoad(canvasTransform.gameObject);//让canvas对象过场景不移除
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName=typeof(T).Name;
        if (panelDic.ContainsKey(panelName) ) return panelDic[panelName] as T;//如果该面板已显示，就无需再创建了，直接返回
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));//根据类名panelName动态创建面板
        panelObj.transform.SetParent(canvasTransform, false);//把该面板放在canvas里
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);//把面板加到当前显示面板容器中
        panel.ShowMe();//开始淡入，alpha从0变1
        return panel;
        
    }
    public void HidePanel<T>(bool isFade = true)where T : BasePanel//淡出传true，直接隐藏传false
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))//判断当前显示的面板中，有无该面板
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
