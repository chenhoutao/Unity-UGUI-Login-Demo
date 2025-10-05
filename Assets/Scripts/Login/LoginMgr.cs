using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr
{
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance=>instance;
    //��¼
    private LoginData loginData;
    public LoginData LoginData=>loginData;
    //ע��
    private RegData regData;
    public RegData RegData=>regData;

    private LoginMgr() 
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        regData = JsonMgr.Instance.LoadData<RegData>("RegData");
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");

    }
    //����������
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData=>serverData;


    #region ��¼
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

    #region ע��(�������桢��֤)
    public void SaveRegData()//����ע������
    {
        JsonMgr.Instance.SaveData(regData, "RegData");
    }
    public bool RegUser(string userName, string passWord)//����ע��
    {
        if(regData.regInfo.ContainsKey(userName))//�û����ڷ���false
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
