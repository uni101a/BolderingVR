using UnityEngine;
using VRTK;

/**
握った手のHPを回復するホールド

Attach to 
*/
class HealingHold : Hold
{
    #pragma warning disable 0649

    private HPManager hPManager;

    private float healVal = 35;

    void Awake()
    {
        interactableObjectScript = GetComponent<VRTK_InteractableObject>();
        material = GetComponent<Renderer>().material;
        scoreManager = ScoreManager.GetScoreInstance();
        hPManager = HPManager.GetHPInstance();
    }

    void Update()
    {
        //ホールドが掴まれたとき
        if(GetIsGrabble()){
            //ホールドのスキルが一度も使われていない（このホールドを握ったのが初めて）ときスキルを発動
            if(isNotActivated){
                //メンバ変数の値をセット
                SetActivateGrabble();
                Heal();
            }
        }

        SetIsNotActivated();
    }

    private void Heal(){
        scoreManager.IncrementCombo();
        if(handType == "left"){
            hPManager.HealHandsHP("left", healVal);
        }else if(handType == "right"){
            hPManager.HealHandsHP("right", healVal);
        }
    }
}
