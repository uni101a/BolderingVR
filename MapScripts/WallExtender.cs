using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WallExtender : MonoBehaviour
{
    public GameObject wall;

    private Material material;

    private WallConf wallConf;

    private int _height = 25;

    private int tileNum = 1;

    void Awake()
    {
        wallConf = WallConf.GetInstance();
    }

    void Start()
    {
        //material = wall.transform.Find("wall").gameObject.GetComponent<Renderer>().material;
    }

    public void ReceiveNotify(){
        ExtendWallHeight();
    }

    private void ExtendWallHeight(){
        _height += wallConf.GetEXTEND_Y_LENGTH();
        wall.transform.localScale = new Vector3(wallConf.GetWALL_X(), _height, 1);
        //material.mainTextureScale = new Vector2(1, ++tileNum);
    }
}
