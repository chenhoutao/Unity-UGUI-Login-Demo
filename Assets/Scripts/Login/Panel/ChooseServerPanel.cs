using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    public Button btnBack;
    //���ҵĹ�����ͼ
    public ScrollRect svLeft;
    public ScrollRect svRight;
    //�ϴε�¼
    public Text txtName;
    public Image imgState;
    //��ǰѡ�еķ�������Χ
    public Text txtRange;
    //���ҲఴťȺ
    private List<GameObject> itemList = new List<GameObject>(); 
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            //���ص�¼ҳ��
            UIManager.Instance.ShowPanel<ServerPanel>();
            //���ظ�ҳ��
            UIManager.Instance.HidePanel<ChooseServerPanel>();
        }
     );
        //��̬�������ķ���������ѡ��ť
        //��ȡ�������б�����
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        print("����������"+infoList.Count);
        //�õ�һ����Ҫѭ���������ٸ����䰴ť,Ĭ��5����һ������
        int groupSize = 8;
        bool duoYi = false;
        if(infoList.Count%groupSize==1) duoYi = true;
        for (int start = 0;start<infoList.Count; start+=groupSize)
        {
            //��̬������ఴť
             GameObject item = Instantiate(Resources.Load<GameObject>("UI/BtnServerLeft"));
            //���ø�����Ҫ��������content�������
            item.transform.SetParent(svLeft.content, false);
            //��ʼ��
            BtnServerLeft btnServerLeft = item.GetComponent<BtnServerLeft>();
            int beginIndex = start+1;
            int endIndex = Mathf.Min(start + groupSize, infoList.Count);
            //��ʼ�����䰴ť
            if(duoYi&&start+groupSize+1==infoList.Count)
            {
                btnServerLeft.InitInfo(beginIndex, endIndex+1);
                break;
            }
            btnServerLeft.InitInfo(beginIndex, endIndex);
        }
    }
    public override void ShowMe()
    {
        base.ShowMe();
        #region �����ϴε�¼��Ϣ
        int id =LoginMgr.Instance.LoginData.frontServerID;
        //��ʾ�Լ�ʱ��ʼ���ϴ�ѡ��ķ�����
        if (id <= 0)//���ϴ�δ��¼
        {
            txtName.text = "��";
            imgState.gameObject.SetActive(false);//�����������״̬���
        }
        else
        {
            ServerInfo serverInfo = LoginMgr.Instance.ServerData[id - 1];
            txtName.text=serverInfo.id+"�� "+serverInfo.name;
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
        #endregion
        #region ���µ�ǰѡ�������
        UpdatePanel(1, 8 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 8);
        #endregion
    }

    //������������ѡ��ť������ShowMeʱ�����Ҳ��·������
    public void UpdatePanel(int beginIndex, int endIndex)
    {
        txtRange.text = "������ "+beginIndex + "-" + endIndex;
        //ɾ��֮ǰ�İ�ť
        for(int i=0;i<itemList.Count;i++)
        {
            Destroy(itemList[i]);
        }
        itemList.Clear();
        for (int i = beginIndex; i <= endIndex; i++)
        {
            ServerInfo currentInfo = LoginMgr.Instance.ServerData[i-1];
            //��̬����Ԥ���尴ť
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/BtnServerRight"));
            serverItem.transform.SetParent(svRight.content,false);
            //���°�ť����
            BtnServerRight btnRight = serverItem.GetComponent<BtnServerRight>();
            btnRight.InitInfo(currentInfo);

            //�����ɹ��������¼���б���
            itemList.Add(serverItem);
        }
    }
}
