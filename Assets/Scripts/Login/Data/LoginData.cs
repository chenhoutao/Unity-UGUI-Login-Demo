using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//记录上次登录玩家的偏好设置
public class LoginData
{
    public string userName;
    public string passWord;

    public bool rememberPW;
    public bool autoLogin;

    //服务器相关
    public int frontServerID = 0;//未选择过服务器

}
