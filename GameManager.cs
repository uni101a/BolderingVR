using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
Attach : GameManager
*/
class GameManager : MonoBehaviour
{
    #pragma warning disable 0649

    //PlayAreaオブジェクトにアタッチしたVRTK_PlayerClimbを参照
    public VRTK_PlayerClimb playerClimbScript;

    //singleton
    private ClimbingStatus climbingStatus;
    //hpを管理するスクリプト.singleton
    private HPManager hPManager;
    //scoreを管理するスクリプト.singleton
    private ScoreManager scoreManager;

    //ゲームが開始されているかの状態
    private bool isStartedGame = false;


    /**
    Start()より先に呼ばれる
    climbingStatusを初期化
    */
    void Awake()
    {
        climbingStatus = ClimbingStatus.GetInstance(playerClimbScript);
        hPManager = HPManager.GetHPInstance();
        scoreManager = ScoreManager.GetScoreInstance();
    }

    void Start()
    {
        hPManager.SetLeftHP(100);
        hPManager.SetRightHP(100);
    }

    void Update()
    {
        //ゲーム開始. 以降の処理は全てゲーム開始後について
        if(!isStartedGame && climbingStatus.GetIsClimbing()){
            isStartedGame = true;
        }

        if(isStartedGame){
            //どちらの手もホールドから離れたときゲームオーバー
            if(!climbingStatus.GetIsClimbing()){
                DestroySingleton();
                SceneManager.LoadScene("gameover");
                return;
            }

            //ホールドを握っている手のhpを変更
            if(GetControllerType(GetClimbingController()) == "left"){
                ReduceGrabbingHandsHP("left"); //握っている手のhpを減少
                HealHandsHP("right"); //握っていない手のhpを回復
            }
            if(GetControllerType(GetClimbingController()) == "right"){
                ReduceGrabbingHandsHP("right");
                HealHandsHP("left");
            }
        }
    }

    public ClimbingStatus GetClimbingStatusInstance(){
        return climbingStatus;
    }

    public bool GetIsStartedGame(){
        return isStartedGame;
    }

    /**
    現在握っているコントローラーのオブジェクトを返す
    */
    public GameObject GetClimbingController(){
        return climbingStatus.GetClimbingController();
    }

    /**
    return "left" or "right"
    */
    public string GetControllerType(GameObject controller){
        if(controller.name.Contains("left")){
            return "left";
        }else{
            return "right";
        }
    }

    public void ReduceGrabbingHandsHP(string controllerType){
        hPManager.ReduceGrabbingHandsHP(controllerType, hPManager.GetContinuouslyReduceVal());
    }

    public void HealHandsHP(string controllerType){
        hPManager.HealHandsHP(controllerType, hPManager.GetContinuouslyHealVal());
    }

    private void DestroySingleton(){
        TimeManager.DestroyInstance();
        HPManager.DestroyInstance();
        ClimbingStatus.DestroyInstance();
    }
}