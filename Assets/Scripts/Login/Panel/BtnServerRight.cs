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
    
    public ServerInfo currentServerInfo;//当前按钮代表的服务器

    public void InitInfo(ServerInfo serverInfo)//初始化，更新按钮显示相关
    {
        currentServerInfo = serverInfo;
        txtName.text= serverInfo.id+"区 "+ serverInfo.name;//按钮显示的区服名
        imgNew.gameObject.SetActive(serverInfo.isNew);//是否是新服
        //状态设置
        imgState.gameObject.SetActive(true);
        //加载图集
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Login");
        switch (serverInfo.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1://流畅
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_liuchang_01");
                break;
            case 2://繁忙
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_fanhua_01");
                break;
            case 3://火爆
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_huobao_01");
                break;
            case 4://维护
                imgState.sprite = spriteAtlas.GetSprite("ui_DL_weihu_01");
                break;
        }
    }

    #region 生命周期函数
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //记录当前用户选中的服务器
            LoginMgr.Instance.LoginData.frontServerID = currentServerInfo.id;
            //隐藏选服面板
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            //显示服务器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        }
        );
    }

    void Update()
    {
        
    }
    #endregion
}
