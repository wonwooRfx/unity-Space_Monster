using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnclickStartBtn()
    {
        Application.LoadLevel("NoStage");
        Application.LoadLevelAdditive("OnlyStage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
