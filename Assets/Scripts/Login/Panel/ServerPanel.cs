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
            //返回登录页面
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏该页面
            UIManager.Instance.HidePanel<ServerPanel>();
        }
        );
        btnEnterGame.onClick.AddListener(() => 
        {
            //进入游戏
            //隐藏选服面板和背景图面板
            UIManager.Instance.HidePanel<ServerPanel>();
            UIManager.Instance.HidePanel<BackPanel>();
            //存当前显示选择的服务器id
            LoginMgr.Instance.SaveLoginData();
            //加载游戏界面
            SceneManager.LoadScene("GameScene");
        });
        btnChangeServer.onClick.AddListener(() => 
        {
            //显示服务器列表面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己的时候更新当前选中的服务器名字
        //通过上次记录的服务器ID更新内容
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if(id<=0) txtServerName.text = "请选区";
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtServerName.text = info.id + "区 " + info.name;
        }

            
    }
}
