using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    #region ��ť
    public Button btnReg;//ע�ᰴť
    public Button btnSure;//ȷ����ť
    #endregion

    #region �����
    public InputField inputUN;//�˺�
    public InputField inputPW;//����
    #endregion

    #region ��ѡ��
    public Toggle toggleRemberPW;//��ס����
    public Toggle toggleAutoLogin;//�Զ���¼
    #endregion

    public override void Init()
    {
        //���ע��
        btnReg.onClick.AddListener(() =>
        {
            //��ʾע�����
            UIManager.Instance.ShowPanel<RegPanel>();
            //���ص�¼���
            UIManager .Instance.HidePanel<LoginPanel>();
        });

        //���ȷ������¼��
        btnSure.onClick.AddListener(() =>
        {
            //��֤�û������Ƿ�Ϸ�
            if (inputPW.text.Length < 6 || inputUN.text.Length < 6)
            {
                //����ʾ��壬��ʾ���Ϸ�
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("�û��������������6λ������������");
                return;
            }

            //��֤�û����������Ƿ�ͨ��
            if(LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                //��¼�ɹ�
                //���浱ǰ����
                LoginMgr.Instance.LoginData.userName=inputUN.text;
                LoginMgr.Instance.LoginData.passWord=inputPW.text;
                LoginMgr.Instance.LoginData.rememberPW = toggleRemberPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = toggleAutoLogin.isOn;
                LoginMgr.Instance.SaveLoginData();
                //��ʾ��¼�ɹ�
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("��¼�ɹ�",()=>
                {
                    //���ݷ�������Ϣ��ȷ����ʾ�ĸ����
                    if (LoginMgr.Instance.LoginData.frontServerID <=0)
                    {
                        //����δѡ���������,ֱ�Ӵ�ѡ�����
                        UIManager.Instance.ShowPanel<ChooseServerPanel>();
                    }
                    else
                    {
                        //�򿪷��������
                        UIManager.Instance.ShowPanel<ServerPanel>();
                    }
                }
                );
                //���ص�¼����
                UIManager.Instance.HidePanel<LoginPanel>();
                

            }
            else
            {
                //��¼ʧ��
                //��ʾ��¼ʧ��,�������
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("��¼ʧ�ܣ���֤���ٵ�¼");
                //������
                inputPW.text = "";

            }
          
        });

        //�����ס����toggle
        toggleRemberPW.onValueChanged.AddListener((isOn) =>
        {
            //��ȡ����ס���룬��ȡ���Զ���¼
            if (!isOn)
            {
                toggleAutoLogin.isOn = false;
            }
        });

        //����Զ���¼toggle
        toggleAutoLogin.onValueChanged.AddListener((isOn) =>
        {
            //��ѡ���Զ���¼ʱ����ס����Ӧ���Զ���ѡ��
            if (isOn)
            {
                toggleRemberPW.isOn = true;
            }
        });

    }

    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾʱ��������������ϵ��û�ƫ��
        LoginData loginData = LoginMgr.Instance.LoginData;
        //���¶�ѡ��
        toggleRemberPW.isOn = loginData.rememberPW;
        toggleAutoLogin.isOn=loginData.autoLogin;
        //�����˺�����
        inputUN.text=loginData.userName;
        if(toggleRemberPW.isOn)inputPW.text=loginData.passWord;
        //����ѡ�Զ���¼
        if(toggleAutoLogin.isOn)
        {
            //�Զ�ȥ��֤�˺��������
            if(LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                if(LoginMgr.Instance.LoginData.frontServerID<=0)
                {
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("�˺��������");
            }
            //����ѡ������
        }
    }

    public void SetInfo(string userName,string passWord)
    {
        inputUN.text=userName;
        inputPW.text=passWord;
    }


}
