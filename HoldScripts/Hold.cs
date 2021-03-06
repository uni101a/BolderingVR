using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/**
各ホールドのスーパークラス
*/
class Hold : MonoBehaviour
{
    #pragma warning disable 0649

    //VTRK_InteractableObjectを参照
    protected VRTK_InteractableObject interactableObjectScript;
    protected ScoreManager　scoreManager;

    //ホールドスキルが発動したか
    protected bool isNotActivated = true;
    //どちらの手で握られているか "left" or "right" or "none"
    protected string handType = "none";

    protected Material material;

    /**
    VRTKスクリプトからこのホールドが掴まれているかを返す
    */
    protected bool GetIsGrabble(){
        return interactableObjectScript.IsGrabbed();
    }

    protected void SetActivateGrabble(){
        isNotActivated = false;
        
        if(interactableObjectScript.GetGrabbingObject().name.Contains("Left")){
            handType = "left";
        }else if(interactableObjectScript.GetGrabbingObject().name.Contains("Right")){
            handType = "right";
        }else{
            handType = "none";
        }

        ChangeColor();
    }

    protected bool IsNotActive(){
        if(!gameObject.activeSelf){
            return true;
        }
        return false;
    }

    protected void SetIsNotActivated(Color color){
        if(isNotActivated) return;

        if(IsNotActive()){
            material.color = color;
            isNotActivated = true;
        }
    }

    protected void ChangeColor(){
        material.color = new Color32(245, 222, 179, 1);
    }
}