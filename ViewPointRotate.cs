using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Attach : Scene > VRTK_SDK > SteamVR_SDK > CameraRig > Controller(left or right)
*/
class ViewPointRotate : MonoBehaviour
{
    #pragma warning disable 0649
    
    //コントローラーを認識
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

　　//SteamVR_SDK
    public GameObject player;
    //GameManager
    public GameManager gameManager;

    private ClimbingStatus climbingStatus;
    private bool isRotating = false;

    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        climbingStatus = gameManager.GetClimbingStatusInstance();
    }

    void Update () 
    {
        if(climbingStatus.GetIsClimbing()){
            return;
        }

        //コントローラーを認識
        device = SteamVR_Controller.Input ((int)trackedObject.index);
        //スティックの入力から視点を回転
        RotatePlayerView(device.GetAxis());
    }

    /**
    視点を回転
    */
    private void RotatePlayerView(Vector2 stick){
        // 右スティックのXが -0.9 を超えたなら左45度回転する
        if (stick.x < -0.9 && isRotating == false)
        {
            isRotating = true;
            player.transform.Rotate(0, -45f, 0, Space.Self);
        } 

        // 右スティックのXが 0.9 を超えたなら右45度回転する
        else if (stick.x > 0.9 && isRotating == false)
        {
            isRotating = true;
            player.transform.Rotate(0, 45f, 0, Space.Self);
        }
        // 右スティックが視点回転方向から戻されたと判断したらフラグ解除して次の入力を受け付ける
        else if (stick.x >= -0.9 && stick.x <= 0.9 && isRotating == true)
        {
            isRotating = false;
        }
    }
}
