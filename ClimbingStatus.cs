using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

//This object is applied singleton pattern
class ClimbingStatus
{
    #pragma warning disable 0649
    
    //PlayAreaオブジェクトにアタッチしたVRTK_PlayerClimbを参照
    private VRTK_PlayerClimb playerClimbScript;

    //どちらかの手がホールドを握っているか
    private bool isClimbing;


    // ----------------------- apply singleton -----------------------
    private static ClimbingStatus climbingStatus;

    private ClimbingStatus(VRTK_PlayerClimb playerClimbScript){
        this.playerClimbScript = playerClimbScript;
    }

    public static ClimbingStatus GetInstance(VRTK_PlayerClimb playerClimbScript){
        if(climbingStatus == null){
            climbingStatus = new ClimbingStatus(playerClimbScript);
        }

        return climbingStatus;
    }

    // ----------------------- applied singleton -----------------------
    

    /**
    VRTKスクリプトからclimbingのbool値を返す
    */
    public bool GetIsClimbing(){
        return playerClimbScript.IsClimbing();
    }

    /**
    VRTKスクリプトから現在ホールドを握っている手がどちらかを返す

    return Controller(left) or Controller(right)
    */
    public GameObject GetClimbingController(){
        return playerClimbScript.GetGrabbingController();
    }
    
}