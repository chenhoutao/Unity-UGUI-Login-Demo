using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//面板基类
public abstract class BasePanel : MonoBehaviour
{
    #region 面板属性   
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 5;//面板淡入淡出的速度
    private bool isShow;
    #endregion

    #region 面板控制函数
    private UnityAction hideCallback;//淡出成功时执行的委托函数,用于动态删除面板
    public abstract void Init();
    public virtual void ShowMe()//显示
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    public virtual void HideMe(UnityAction callBack)//隐藏
    {
        isShow=false;
        canvasGroup.alpha = 1;
        hideCallback=callBack;//记录淡出成功时会执行的函数
    }
    #endregion

    #region 生命周期函数
    protected virtual void Awake()
    {
        //获取面板上挂的CanvasGroup组件
        canvasGroup = GetComponent<CanvasGroup>();
        //若未手动添加CanvasGroup组件，则自动添加一个
        if (canvasGroup == null ) canvasGroup=gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    void Update()
    {
        #region 淡入淡出处理
        if ( isShow && canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha>=1) canvasGroup.alpha = 1; 
        }
        else if(!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0) 
            {
                canvasGroup.alpha = 0;
                hideCallback?.Invoke();
            } 
        }
        #endregion
    }
    #endregion
}
