using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnChangeServer;
    public Button btnEnterGame;
    public Button btnBack;
    public Text txtServerName;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            if(LoginMgr.Instance.LoginData.autoLogin) LoginMgr.Instance.LoginData.autoLogin=false;
            //���ص�¼ҳ��
            UIManager.Instance.ShowPanel<LoginPanel>();
            //���ظ�ҳ��
            UIManager.Instance.HidePanel<ServerPanel>();
        }
        );
        btnEnterGame.onClick.AddListener(() => 
        {
            //������Ϸ
            //����ѡ�����ͱ���ͼ���
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.HidePanel<BackPanel>();
            //�浱ǰ��ʾѡ��ķ�����id
            LoginMgr.Instance.SaveLoginData();
            //������Ϸ����
            SceneManager.LoadScene("GameScene");
        });
        btnChangeServer.onClick.AddListener(() => 
        {
            //��ʾ�������б����
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //�����Լ�
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ�Լ���ʱ����µ�ǰѡ�еķ���������
        //ͨ���ϴμ�¼�ķ�����ID��������
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if(id<=0) txtServerName.text = "��ѡ��";
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtServerName.text = info.id + "�� " + info.name;
        }

            
    }
}
