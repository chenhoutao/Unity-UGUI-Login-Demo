using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnSure;
    public Text txtInfo;

    private UnityAction onConfirm;

    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            onConfirm?.Invoke();                      
            onConfirm = null;
            UIManager.Instance.HidePanel<TipPanel>();//点击确认后面板消失
            
        });
    }
    public void ChangeInfo(string info,UnityAction confirmAction = null)//提供给外部使用，可改变提示板内容
    {
        txtInfo.text = info;
        onConfirm = confirmAction;
    }
}
