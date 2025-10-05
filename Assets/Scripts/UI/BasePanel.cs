using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//������
public abstract class BasePanel : MonoBehaviour
{
    #region �������   
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 5;//��嵭�뵭�����ٶ�
    private bool isShow;
    #endregion

    #region �����ƺ���
    private UnityAction hideCallback;//�����ɹ�ʱִ�е�ί�к���,���ڶ�̬ɾ�����
    public abstract void Init();
    public virtual void ShowMe()//��ʾ
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    public virtual void HideMe(UnityAction callBack)//����
    {
        isShow=false;
        canvasGroup.alpha = 1;
        hideCallback=callBack;//��¼�����ɹ�ʱ��ִ�еĺ���
    }
    #endregion

    #region �������ں���
    protected virtual void Awake()
    {
        //��ȡ����Ϲҵ�CanvasGroup���
        canvasGroup = GetComponent<CanvasGroup>();
        //��δ�ֶ����CanvasGroup��������Զ����һ��
        if (canvasGroup == null ) canvasGroup=gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    void Update()
    {
        #region ���뵭������
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
