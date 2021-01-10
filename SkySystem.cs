using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class SkySystem : MonoBehaviour
{
    #pragma warning disable 0649

    //Skyboxのマテリアル 朝昼晩(0~2)
    public Material[] materials;
    
    private TimeManager timeManager;

    //太陽が動く間隔
    private float interval;
    //太陽の移動前の時間帯
    private int preTime;

    private bool isNoonSky = false;
    private bool isAfterNoonSky = false;
    private bool isNightSky = false;

    void Start()
    {
        timeManager = TimeManager.GetTimeInstance();
        preTime = timeManager.GetWorldTime();
        isNoonSky = true;
        RenderSettings.skybox = materials[0];
    }
    
    void Update()
    {
        interval += Time.deltaTime;
        if(interval >= 0.05f){
            ForwardTime();
        }

        if(preTime != timeManager.GetWorldTime()){
            int worldTime = timeManager.GetWorldTime();
            preTime = timeManager.GetWorldTime();
            RotateSun(worldTime);
            changeSky(worldTime);
        }
    }

    /**
    時間を進める
    */
    private void ForwardTime(){
        interval = 0;
        timeManager.ForwardWorldTime(1);
    }

    /**
    太陽を回転させる
    */
    private void RotateSun(int worldTime){
        Vector3 vector = new Vector3(worldTime, 0, 0);
        //夜は移動しない
        if(worldTime >= 210 && worldTime <= 330) return;

        this.transform.rotation = Quaternion.AngleAxis(worldTime, new Vector3(1, 0, 0));
    }

    /**
    Skyboxを時間に応じて変更
    */
    private void changeSky(int worldTime){
        //昼から夕方
        if(isNoonSky){
            if(worldTime >= 160){
                isAfterNoonSky = true;
                isNoonSky = false;
                RenderSettings.skybox = materials[1];
            }
        }else if(isAfterNoonSky){//夕方から夜
            if(worldTime >= 180){
                isNightSky = true;
                isAfterNoonSky = false;
                RenderSettings.skybox = materials[2];
            }
        }else if(isNightSky){//夜から昼
            if(worldTime >= 180 && worldTime <= 360) return;
            if(worldTime >= 10){
                isNoonSky = true;
                isNightSky = false;
                RenderSettings.skybox = materials[0];
            }
        }
    }
}