using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class BtnServerRight : MonoBehaviour
{
    public Button btnSelf;
    public Text txtName;
    public Image imgNew;
    public Image imgState;
    
    public ServerInfo currentServerInfo;//��ǰ��ť����ķ�����

    public void InitInfo(ServerInfo serverInfo)//��ʼ�������°�ť��ʾ���
    {
        currentServerInfo = serverInfo;
        txtName.text= serverInfo.id+"�� "+ serverInfo.name;//��ť��ʾ��������
        imgNew.gameObject.SetActive(serverInfo.isNew);//�Ƿ����·�
        //״̬����
        imgState.gameObject.SetActive(true);
        //����ͼ��
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Login");
        switch (serverInfo.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1://����
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://��æ
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://��
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                break;
            case 4://ά��
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                break;
        }
    }

    #region �������ں���
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //��¼��ǰ�û�ѡ�еķ�����
            LoginMgr.Instance.LoginData.frontServerID = currentServerInfo.id;
            //����ѡ�����
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            //��ʾ���������
            UIManager.Instance.ShowPanel<ServerPanel>();
        }
        );
    }

    void Update()
    {
        
    }
    #endregion
}
