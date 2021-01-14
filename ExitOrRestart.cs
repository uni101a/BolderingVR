using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitOrRestart : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    private Button restartButton;
    private Button exitButton;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();

        restartButton = GameObject.Find("Restart").GetComponent<Button>();
        exitButton = GameObject.Find("Exit").GetComponent<Button>();
    }
    
    void Update()
    {
        device = SteamVR_Controller.Input ((int)trackedObject.index);

        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)){
            OnClickRestart();
        }

        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)){
            OnClickExit();
        }
    }

    void OnClickRestart(){
        restartButton.onClick.Invoke();
    }

    void OnClickExit(){
        exitButton.onClick.Invoke();
    }
}