using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Power : MonoBehaviour
{
    private Text powerText;
    private Image image;

    private HPManager hPManager;

    private string controllerType;

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        powerText = transform.GetChild(1).GetComponent<Text>();
        hPManager = HPManager.GetHPInstance();
        
        if(this.name.Contains("left")){
            controllerType = "left";
        }else if(this.name.Contains("right")){
            controllerType = "right";
        }else{
            controllerType = "None";
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePower();
    }
    void UpdatePower(){
        int power = (int)GetPowerValue(); // power取得
        if(power <= 20) {
            image.color = Color.red;
            powerText.color = Color.red;
        }
        else if(power <= 50) {
            image.color = Color.yellow;
            powerText.color = Color.yellow;
        }
        else {
            image.color = Color.green;
            powerText.color = Color.green;
        }
        image.fillAmount = power / 100.0f;
        powerText.text = power + "%";
    }

    float GetPowerValue(){
        float hpVal = 0;

        if(controllerType == "left"){
            hpVal = hPManager.GetLeftHP();
        }else if(controllerType == "right"){
            hpVal = hPManager.GetRightHP();
        }

        return hpVal;
    }
}