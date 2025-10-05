using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInfo
{
    public int id;//区号
    public string name;//区名
    public int state;//当前状态5个：无状态、繁忙、火爆、维护
    public bool isNew;//是否是新区
}
