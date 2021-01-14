using UnityEngine;
using VRTK;

/**
握られたとき通常の4倍のスコアを加算するホールド

Attach to 
*/
class FourfoldScoreHold : ScoreHold
{
    #pragma warning disable 0649

    private int scoreRate = 4;

    void Awake()
    {
        interactableObjectScript = GetComponent<VRTK_InteractableObject>();
        material = GetComponent<Renderer>().material;
        scoreManager = ScoreManager.GetScoreInstance();
    }

    void Update()
    {
        //ホールドが掴まれたとき
        if(GetIsGrabble()){
            //ホールドのスキルが一度も使われていない（このホールドを握ったのが初めて）ときスキルを発動
            if(isNotActivated){
                //メンバ変数の値をセット
                SetActivateGrabble();
                IncreaseScore();
            }
        }

        SetIsNotActivated(new Color32(30, 150, 220, 1));
    }

    /**
    スコアを増加するスキル
    */
    protected override void IncreaseScore(){
        scoreManager.IncrementCombo();
        scoreManager.IncreaseTotalScore(scoreRate);
    }
}
