using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpawnHolder : MonoBehaviour
{
    private GameObject gaba;
    private GameObject kati;
    private GameObject pinti;
    private GameObject poket;
    private GameObject sropa;

    private WallConf wallConf;
    private int height = 0;

    void Awake()
    {
        wallConf = WallConf.GetInstance();

        //スコア2倍
        gaba = (GameObject)Resources.Load("HoldGaba");
        //スコア4倍
        kati = (GameObject)Resources.Load("HoldKati");
        //体力を回復
        pinti = (GameObject)Resources.Load("HoldPinti");
        //ダメージを与える
        poket = (GameObject)Resources.Load("HoldPoket");
        //時間を進める
        sropa = (GameObject)Resources.Load("HoldSropa");
    }

    void Start()
    {   
        StartExtend();
    }

    private void StartExtend(){
        ReceiveNotify();
    }

    public void ReceiveNotify(){
        for(float y=0; y<wallConf.GetEXTEND_Y_LENGTH(); y++){
            SpawnLowHolds(y);
        }
    }

    private void SpawnLowHolds(float y){
        for(float x=0; x<wallConf.GetWALL_X(); x=x+2.0f){
            float randX = Random.Range(x, x+3.0f);
            float randY = Random.Range(y, y+1.0f);

            Spawn(randX, randY);
        }

        height += wallConf.GetEXTEND_Y_LENGTH();
    }

    private void Spawn(float x, float y){
        float rand = Random.value;
        GameObject hold;

        if(0.0f <= rand && rand <= 0.1f){
            hold = sropa;//タイムホールド
        }else if(0.1f < rand && rand <= 0.25f){
            hold = poket;//ダメージホールド
        }else if(0.25f < rand && rand <= 0.4f){
            hold = pinti;//ヒールホールド
        }else if(0.4f < rand && rand <= 0.7f){
            hold = kati;//4倍ホールド
        }else{
            hold = gaba;//2倍ホールド
        }

        GenerateHold(hold, x, y);
    }

    private void GenerateHold(GameObject hold, float x, float y){
        InstantiateHold(hold, x, y);
    }

    private void InstantiateHold(GameObject hold, float x, float y){
        float vecx = x - wallConf.GetWALL_X()/2 + 0.4f;
        float vecy = y + 0.5f + 0.25f;
        float vecz = wallConf.Get_WALL_Z_POSITION() - 0.6f;
        GameObject holdPrefab = (GameObject)Instantiate(hold,new Vector3(vecx,vecy,vecz),Quaternion.identity);
    }
}
