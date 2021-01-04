using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class Sun : MonoBehaviour
{
    #pragma warning disable 0649
    
    private TimeManager timeManager;

    private float interval;
    private int preTime;

    void Start()
    {
        timeManager = TimeManager.GetTimeInstance();
        preTime = timeManager.GetWorldTime();
    }
    
    void Update()
    {
        interval += Time.deltaTime;
        if(interval >= 2f){
            ForwardTime();
        }

        if(preTime != timeManager.GetWorldTime()){
            int worldTime = timeManager.GetWorldTime();
            preTime = timeManager.GetWorldTime();
            RotateSun(worldTime);
        }
    }

    private void ForwardTime(){
        interval = 0;
        timeManager.ForwardWorldTime(1);
    }

    private void RotateSun(int worldTime){
        Vector3 vector = new Vector3(worldTime, 0, 0);
        if(worldTime >= 210 && worldTime <= 330) return;

        this.transform.rotation = Quaternion.AngleAxis(worldTime, new Vector3(1, 0, 0));
    }
}