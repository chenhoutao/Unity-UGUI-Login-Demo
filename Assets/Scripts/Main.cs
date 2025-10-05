using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
        //tipPanel.ChangeInfo("tst");
        UIManager.Instance.ShowPanel<BackPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
