using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance=>instance;
    //登录
    private LoginData loginData;
    public LoginData LoginData=>loginData;
    //注册
    private RegData regData;
    public RegData RegData=>regData;

    private LoginMgr() 
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        regData = JsonMgr.Instance.LoadData<RegData>("RegData");
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");

    }
    //服务器数据
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData=>serverData;


    #region 登录
    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.autoLogin = false;
        loginData.rememberPW = false;
    }
    #endregion

    #region 注册(处理、保存、验证)
    public void SaveRegData()//保存注册数据
    {
        JsonMgr.Instance.SaveData(regData, "RegData");
    }
    public bool RegUser(string userName, string passWord)//处理注册
    {
        if(regData.regInfo.ContainsKey(userName))//用户存在返回false
        {
            return false;
        }
        regData.regInfo.Add(userName, passWord);
        SaveRegData();
        return true;
    }
    public bool CheckInfo(string userName,string passWord)
    {
        if(regData.regInfo.ContainsKey(userName))
        {
            if(regData.regInfo[userName] == passWord) return true;
        }
        return false;
    }
    #endregion
}
