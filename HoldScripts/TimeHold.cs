using UnityEngine;
using VRTK;

/**
握られたときゲームの時間を進めるホールド

Attach to 
*/
class TimeHold : Hold
{
    #pragma warning disable 0649

    private TimeManager timeManager;

    private int forwardTime = 100;

    void Awake()
    {
        interactableObjectScript = GetComponent<VRTK_InteractableObject>();
        scoreManager = ScoreManager.GetScoreInstance();
        timeManager = TimeManager.GetTimeInstance();
    }

    void Update()
    {
        //ホールドが掴まれたとき
        if(GetIsGrabble()){
            //ホールドのスキルが一度も使われていない（このホールドを握ったのが初めて）ときスキルを発動
            if(isNotActivated){
                //メンバ変数の値をセット
                SetActivateGrabble();
                ForwardTime();
            }
        }
    }

    private void ForwardTime(){
        scoreManager.IncrementCombo();
        timeManager.ForwardWorldTime(forwardTime);
    }
}
