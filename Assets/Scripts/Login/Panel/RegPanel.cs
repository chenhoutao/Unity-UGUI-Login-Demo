using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegPanel : BasePanel
{
    //ȷ��ȡ����ť
    public Button btnSure;
    public Button btnCancel;
    //�˺����������
    public InputField inputUN;
    public InputField inputPW;
    public override void Init()
    {
        btnSure.onClick.AddListener(() => 
        {
            //�ж��û����������Ƿ����
            if(inputPW.text.Length<6||inputUN.text.Length<6)
            {
                //����ʾ��壬��ʾ���Ϸ�
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("�û��������������6λ������������");
            }
            //ע���˺�
            else if(LoginMgr.Instance.RegUser(inputUN.text,inputPW.text))
            {
                //ע��ɹ�
                //��ʾע��ɹ�
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("ע��ɹ�", () =>
                {
                    //����ǰ�û���¼��
                    LoginMgr.Instance.ClearLoginData();
                    //��ʾʾ��¼���
                    LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                    //���µ�¼����ϵ��û���������
                    loginPanel.SetInfo(inputUN.text, inputPW.text);
                    //����ע��ҳ��
                    UIManager.Instance.HidePanel<RegPanel>();
                }
             );
            }
            else
            {
                //��ʾ�û����Ѵ���
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("�û����Ѵ��ڣ�");
                //��յ�ǰҳ����û���������
                inputPW.text = "";
                inputUN.text = "";
            }
        });
        btnCancel.onClick.AddListener(() => 
        {
            //�����Լ�
            UIManager.Instance.HidePanel<RegPanel>();
            //���ص�¼���
            UIManager.Instance.ShowPanel<LoginPanel>();

        });
    }
}
