using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpawnHolder : MonoBehaviour
{
    private ObjectPool _objectPool; //オブジェクトプール
    private WallConf wallConf; //壁などについての固有値を持ったインスタンス
    private int height = 0; 
    private int ColumnSpawnHoldNum = 6; //列に対してのホールドの数

    private float _holdScaleX = 0.5f; 
    private float _holdScaleY = 0.35f;
    private float _holdScaleZ = 0.2f;

    void Awake()
    {
        wallConf = WallConf.GetInstance();
    }

    void Start()
    {   
        _objectPool = FindObjectOfType<ObjectPool>();
        StartExtend();
    }

    /**
    ローディング時にホールドを生成
    */
    private void StartExtend(){
        SpawnRowHolds();
        SpawnRowHolds();
        //for(int n=0; n<25/wallConf.GetEXTEND_Y_LENGTH()-1; n++){
        //    SpawnRowHolds();
        //}
    }

    /**
    オブザーバーから通知を受けたとき実行
    */
    public void ReceiveNotify(){
        SpawnRowHolds();

        if(height -10 > wallConf.GETWALL_Y()/wallConf.GetEXTEND_Y_LENGTH()){
            int rowColumn = (wallConf.GetEXTEND_Y_LENGTH()/2+1)*ColumnSpawnHoldNum;
            _objectPool.SetNonActiveHolds(height-rowColumn-5, rowColumn);
        }
    }

    //ホールドの生成
    //Y行X列のホールドの団体を生成

    /**
    行に対してホールドを生成
    一回で生成するホールドの行数はEXTEND_Y_LENGTH/2+1
    */
    private void SpawnRowHolds(){
        for(float y=1; y<=wallConf.GetEXTEND_Y_LENGTH()/2+1; y++){
            SpawnColumnHolds(y);
        }

        //プレイヤーの位置を変更
        height += wallConf.GetEXTEND_Y_LENGTH();
    }

    /**
    列に対してのホールドを生成
    生成するホールドの数はColumnSpawnHoldNum
    */
    private void SpawnColumnHolds(float y){
        for(float x=0; x<ColumnSpawnHoldNum; x++){
            Spawn(DefHoldWidth(x), DefHoldHeight(y));
        }
    }

    /**
    生成したホールドのx座標
    */
    private float DefHoldWidth(float x){
        float randX = Random.Range(0, 0.5f);

        return x*2f + randX;
    }

    /**
    生成したホールドのY座標
    */
    private float DefHoldHeight(float y){
        float randY = Random.Range(0, 0.5f);

        return y*1.8f + randY;
    }

    /**
    ホールドを生成

    @param x: ホールドのx座標
    @param y: ホールドのy座標
    */
    private void Spawn(float x, float y){
        float rand = Random.value; //どのホールドを生成するか
        string holdTag; //生成したホールドの種類

        if(0.0f <= rand && rand <= 0.1f){ //10%
            holdTag = "sropa";//タイムホールド
        }else if(0.1f < rand && rand <= 0.25f){ //15%
            holdTag = "poket";//ダメージホールド
        }else if(0.25f < rand && rand <= 0.45f){ //20%
            holdTag = "pinti";//ヒールホールド
        }else if(0.45f < rand && rand <= 0.75f){ //25%
            holdTag = "kati";//4倍ホールド
        }else{ //30%
            holdTag = "gaba";//2倍ホールド
        }

        SetActiveHold(holdTag, x, y);
    }

    /**
    オブジェクトプールからホールドのアクティブを変え位置を変更することでホールドを使い回して生成

    @param holdTag: 生成したいホールドの種類
    */
    private void SetActiveHold(string holdTag, float x, float y){
        float vectorX = x - 5 + _holdScaleX;//7???
        float vectorY = y + height + _holdScaleY;
        float vectorZ = wallConf.Get_WALL_Z_POSITION() - (_holdScaleZ + 0.3f);//0.4はいい感じの位置に調整

        Vector3 position = new Vector3(vectorX,vectorY,vectorZ);
        Quaternion rotation = Quaternion.Euler(180f, 0f, 0f);

        GameObject hold = _objectPool.GetGameObject(holdTag);
        hold.transform.position = position;
        hold.transform.rotation = rotation;
    }
}
