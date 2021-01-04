using UnityEngine;

class HeadLight : MonoBehaviour
{
    #pragma warning disable 0649
    
    private TimeManager timeManager;

    private Light light;

    private bool isLighting = false;

    void Start()
    {
        timeManager = TimeManager.GetTimeInstance();
        light = this.GetComponent<Light>();
        SetLighting();
    }

    void Update()
    {
        if(!isLighting){
            if(timeManager.GetWorldTime() >= 180){
                isLighting = true;
                SetLighting();
            }
        }else{
            if(timeManager.GetWorldTime() <= 10){
                isLighting = false;
                SetLighting();
            }
        }
    }

    private void SetLighting(){
        light.enabled = isLighting;
    }
}