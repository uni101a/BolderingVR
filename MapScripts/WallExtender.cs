using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WallExtender : MonoBehaviour
{
    public GameObject wall;

    private WallConf wallConf;

    private int _height = 20;

    void Awake()
    {
        wallConf = WallConf.GetInstance();
    }

    public void ReceiveNotify(){
        ExtendWallHeight();
    }

    private void ExtendWallHeight(){
        _height += wallConf.GetEXTEND_Y_LENGTH();
        wall.transform.localScale = new Vector3(wallConf.GetWALL_X(), _height, 1);
    }
}
