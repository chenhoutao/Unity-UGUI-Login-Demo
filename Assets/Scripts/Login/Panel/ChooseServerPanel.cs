using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    public Button btnBack;
    //左右的滚动视图
    public ScrollRect svLeft;
    public ScrollRect svRight;
    //上次登录
    public Text txtName;
    public Image imgState;
    //当前选中的服务器范围
    public Text txtRange;
    //存右侧按钮群
    private List<GameObject> itemList = new List<GameObject>(); 
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            //返回登录页面
            UIManager.Instance.ShowPanel<ServerPanel>();
            //隐藏该页面
            UIManager.Instance.HidePanel<ChooseServerPanel>();
        }
     );
        //动态创建左侧的服务器区间选择按钮
        //获取服务器列表数据
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        print("数据数量："+infoList.Count);
        //得到一共需要循环创建多少个区间按钮,默认5个服一个区间
        int groupSize = 8;
        bool duoYi = false;
        if(infoList.Count%groupSize==1) duoYi = true;
        for (int start = 0;start<infoList.Count; start+=groupSize)
        {
            //动态创建左侧按钮
             GameObject item = Instantiate(Resources.Load<GameObject>("UI/BtnServerLeft"));
            //设置父对象，要把它放在content里才有用
            item.transform.SetParent(svLeft.content, false);
            //初始化
            BtnServerLeft btnServerLeft = item.GetComponent<BtnServerLeft>();
            int beginIndex = start+1;
            int endIndex = Mathf.Min(start + groupSize, infoList.Count);
            //初始化区间按钮
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
        #region 更新上次登录信息
        int id =LoginMgr.Instance.LoginData.frontServerID;
        //显示自己时初始化上次选择的服务器
        if (id <= 0)//若上次未登录
        {
            txtName.text = "无";
            imgState.gameObject.SetActive(false);//不激活服务器状态组件
        }
        else
        {
            ServerInfo serverInfo = LoginMgr.Instance.ServerData[id - 1];
            txtName.text=serverInfo.id+"区 "+serverInfo.name;
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
        #endregion
        #region 更新当前选择的区间
        UpdatePanel(1, 8 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 8);
        #endregion
    }

    //点了左侧的区间选择按钮，或者ShowMe时更新右侧下方大面板
    public void UpdatePanel(int beginIndex, int endIndex)
    {
        txtRange.text = "服务器 "+beginIndex + "-" + endIndex;
        //删除之前的按钮
        for(int i=0;i<itemList.Count;i++)
        {
            Destroy(itemList[i]);
        }
        itemList.Clear();
        for (int i = beginIndex; i <= endIndex; i++)
        {
            ServerInfo currentInfo = LoginMgr.Instance.ServerData[i-1];
            //动态创建预设体按钮
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/BtnServerRight"));
            serverItem.transform.SetParent(svRight.content,false);
            //更新按钮数据
            BtnServerRight btnRight = serverItem.GetComponent<BtnServerRight>();
            btnRight.InitInfo(currentInfo);

            //创建成功后把它记录在列表中
            itemList.Add(serverItem);
        }
    }
}
