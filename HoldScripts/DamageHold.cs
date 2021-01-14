using UnityEngine;
using VRTK;

/**
握った手のHPを減らすホールド

Attach to 
*/
class DamageHold : Hold
{
    #pragma warning disable 0649

    private HPManager hPManager;

    private float damageVal = 20;

    void Awake()
    {
        interactableObjectScript = GetComponent<VRTK_InteractableObject>();
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
                Damage();
            }
        }

        SetIsNotActivated();
    }

    private void Damage(){
        scoreManager.ResetCombo();
        if(handType == "left"){
            hPManager.ReduceGrabbingHandsHP("left", damageVal);
        }else if(handType == "right"){
            hPManager.ReduceGrabbingHandsHP("right", damageVal);
        }
    }
}
