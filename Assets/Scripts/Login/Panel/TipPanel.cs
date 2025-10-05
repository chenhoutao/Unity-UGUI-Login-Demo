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
            UIManager.Instance.HidePanel<TipPanel>();//���ȷ�Ϻ������ʧ
            
        });
    }
    public void ChangeInfo(string info,UnityAction confirmAction = null)//�ṩ���ⲿʹ�ã��ɸı���ʾ������
    {
        txtInfo.text = info;
        onConfirm = confirmAction;
    }
}
