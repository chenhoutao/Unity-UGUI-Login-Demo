using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//��Ϊѡ��������ఴť
//����˰�ť����ʾĳһ���������ڵķ������İ�ť
public class BtnServerLeft : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;//��ʾ��������������

    private int beginIndex;
    private int endIndex;

    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        txtInfo.text = beginIndex + "-" + endIndex + "��";
    }

    #region �������ں���
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //֪ͨѡ�����ı��Ҳ���������
            ChooseServerPanel chooseServerPanel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            chooseServerPanel.UpdatePanel(beginIndex, endIndex);
        }
        );
    }
    void Update()
    {
        
    }
    #endregion
}
