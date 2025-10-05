using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    #region 按钮
    public Button btnReg;//注册按钮
    public Button btnSure;//确定按钮
    #endregion

    #region 输入框
    public InputField inputUN;//账号
    public InputField inputPW;//密码
    #endregion

    #region 多选框
    public Toggle toggleRemberPW;//记住密码
    public Toggle toggleAutoLogin;//自动登录
    #endregion

    public override void Init()
    {
        //点击注册
        btnReg.onClick.AddListener(() =>
        {
            //显示注册面板
            UIManager.Instance.ShowPanel<RegPanel>();
            //隐藏登录面板
            UIManager .Instance.HidePanel<LoginPanel>();
        });

        //点击确定（登录）
        btnSure.onClick.AddListener(() =>
        {
            //验证用户密码是否合法
            if (inputPW.text.Length < 6 || inputUN.text.Length < 6)
            {
                //出提示面板，提示不合法
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用户名或密码须大于6位，请重新输入");
                return;
            }

            //验证用户名和密码是否通过
            if(LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                //登录成功
                //保存当前数据
                LoginMgr.Instance.LoginData.userName=inputUN.text;
                LoginMgr.Instance.LoginData.passWord=inputPW.text;
                LoginMgr.Instance.LoginData.rememberPW = toggleRemberPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin = toggleAutoLogin.isOn;
                LoginMgr.Instance.SaveLoginData();
                //提示登录成功
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("登录成功",()=>
                {
                    //根据服务器信息来确定显示哪个面板
                    if (LoginMgr.Instance.LoginData.frontServerID <=0)
                    {
                        //从来未选择过服务器,直接打开选服面板
                        UIManager.Instance.ShowPanel<ChooseServerPanel>();
                    }
                    else
                    {
                        //打开服务器面板
                        UIManager.Instance.ShowPanel<ServerPanel>();
                    }
                }
                );
                //隐藏登录界面
                UIManager.Instance.HidePanel<LoginPanel>();
                

            }
            else
            {
                //登录失败
                //提示登录失败,清空密码
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("登录失败，查证后再登录");
                //清空面板
                inputPW.text = "";

            }
          
        });

        //点击记住密码toggle
        toggleRemberPW.onValueChanged.AddListener((isOn) =>
        {
            //若取消记住密码，则取消自动登录
            if (!isOn)
            {
                toggleAutoLogin.isOn = false;
            }
        });

        //点击自动登录toggle
        toggleAutoLogin.onValueChanged.AddListener((isOn) =>
        {
            //当选中自动登录时，记住密码应该自动被选中
            if (isOn)
            {
                toggleRemberPW.isOn = true;
            }
        });

    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示时根据面板更新面板上的用户偏好
        LoginData loginData = LoginMgr.Instance.LoginData;
        //更新多选框
        toggleRemberPW.isOn = loginData.rememberPW;
        toggleAutoLogin.isOn=loginData.autoLogin;
        //更新账号密码
        inputUN.text=loginData.userName;
        if(toggleRemberPW.isOn)inputPW.text=loginData.passWord;
        //若勾选自动登录
        if(toggleAutoLogin.isOn)
        {
            //自动去验证账号密码相关
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
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号密码错误！");
            }
            //进入选服界面
        }
    }

    public void SetInfo(string userName,string passWord)
    {
        inputUN.text=userName;
        inputPW.text=passWord;
    }


}
