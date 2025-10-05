using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegPanel : BasePanel
{
    //确定取消按钮
    public Button btnSure;
    public Button btnCancel;
    //账号密码输入框
    public InputField inputUN;
    public InputField inputPW;
    public override void Init()
    {
        btnSure.onClick.AddListener(() => 
        {
            //判断用户名，密码是否合理
            if(inputPW.text.Length<6||inputUN.text.Length<6)
            {
                //出提示面板，提示不合法
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("用户名或密码须大于6位，请重新输入");
            }
            //注册账号
            else if(LoginMgr.Instance.RegUser(inputUN.text,inputPW.text))
            {
                //注册成功
                //提示注册成功
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("注册成功", () =>
                {
                    //清理前用户登录癖好
                    LoginMgr.Instance.ClearLoginData();
                    //显示示登录面板
                    LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                    //更新登录面板上的用户名和密码
                    loginPanel.SetInfo(inputUN.text, inputPW.text);
                    //隐藏注册页面
                    UIManager.Instance.HidePanel<RegPanel>();
                }
             );
            }
            else
            {
                //提示用户名已存在
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("用户名已存在！");
                //清空当前页面的用户名和密码
                inputPW.text = "";
                inputUN.text = "";
            }
        });
        btnCancel.onClick.AddListener(() => 
        {
            //隐藏自己
            UIManager.Instance.HidePanel<RegPanel>();
            //返回登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();

        });
    }
}
