using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Attach : Scene > VRTK_SDK > SteamVR_SDK > CameraRig > Controller(left or right)

コントローラーのスティック操作で視点を移動
*/
class PlayerMover : MonoBehaviour
{
    #pragma warning disable 0649
    
    //コントローラーを認識
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    //SteamVR_SDK
    public GameObject player;
    //GameManager
    public GameManager gameManager;
    //Camera(head)
    public Camera mainCamera;

    private ClimbingStatus climbingStatus;
    
    //playerオブジェクトのrigidbodyを参照
    private Rigidbody playerRb;
    //playerオブジェクトの移動速度
    float moveSpeed = 2f;
    
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        climbingStatus = gameManager.GetClimbingStatusInstance();
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update () 
    {
        if(climbingStatus.GetIsClimbing()){
            return;
        }
        //コントローラーを認識
        device = SteamVR_Controller.Input ((int)trackedObject.index);
        //コントローラーのスティックの入力を格納
        Vector2 controllerAxis = device.GetAxis();

        //スティックが入力されているとき
        if(controllerAxis != new Vector2(0f, 0f)){
            var velocity = GenerateVelocity(controllerAxis);
            MovePlayer(velocity);
        }
    }

    /**
    移動するためのVector3を生成
    */
    private Vector3 GenerateVelocity(Vector2 stick){
        // カメラの向き算出
        Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)); 
        // 左スティック入力から移動ベクトル生成
        Vector3 moveForward = Vector3.Scale(cameraForward * stick.y + mainCamera.transform.right * stick.x, new Vector3(1, 0, 1)).normalized;
        //オブジェクトに設定
        return moveForward * moveSpeed + new Vector3(0, playerRb.velocity.y, 0);
    }

    /**
    プレイヤーの視点を移動させる
    */
    private void MovePlayer(Vector3 velocity){
        playerRb.velocity = velocity;
    }

}
