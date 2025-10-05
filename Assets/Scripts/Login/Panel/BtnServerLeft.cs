using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//此为选服面板的左侧按钮
//点击此按钮会显示某一区号区间内的服务器的按钮
public class BtnServerLeft : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;//显示服务器区间内容

    private int beginIndex;
    private int endIndex;

    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        txtInfo.text = beginIndex + "-" + endIndex + "区";
    }

    #region 生命周期函数
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //通知选服面板改变右侧区间内容
            ChooseServerPanel chooseServerPanel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            chooseServerPanel.UpdatePanel(beginIndex, endIndex);
        }
        );
    }
    void Update()
    {
        
    }
    #endregion
}
