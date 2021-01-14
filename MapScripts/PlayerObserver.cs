using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerObserver : MonoBehaviour
{
    public GameObject _player; //プレイヤーオブジェクト(CameraRig)
    private SpawnHolder spawnHolder; 
    private WallExtender wallExtender;

    private WallConf wallConf;

    private int _playerYPostion = 0;

    void Awake()
    {
        wallConf = WallConf.GetInstance();
    }

    void Start(){
        spawnHolder = GetComponent<SpawnHolder>();
        wallExtender = GetComponent<WallExtender>();
    }

    void Update(){
        Vector3 vec = _player.transform.position; //現在のプレイヤーの座標を取得
        int playerYPosition = (int)vec.y; //y座標を取得

        //最後の_playerYPositionからExtendedYLength進んでいたらtrue
        if(playerYPosition >= _playerYPostion + wallConf.GetEXTEND_Y_LENGTH()){
            _playerYPostion = playerYPosition;//更新
            NotifyToSubjects();//通知
        }
    }

    private void NotifyToSubjects(){
        wallExtender.ReceiveNotify();
        spawnHolder.ReceiveNotify();
    }
}