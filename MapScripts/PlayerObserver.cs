using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerObserver : MonoBehaviour
{
    public GameObject _player;
    private  SpawnHolder spawnHolder;
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
        Vector3 vec = _player.transform.position;
        int playerYPosition = (int)vec.y;

        if(playerYPosition >= _playerYPostion + wallConf.GetEXTEND_Y_LENGTH()){
            _playerYPostion = playerYPosition + wallConf.GetEXTEND_Y_LENGTH();
            NotifyToSubjects();
        }
    }

    private void NotifyToSubjects(){
        wallExtender.ReceiveNotify();
        spawnHolder.ReceiveNotify();
    }
}