using System;

//This object is applied singleton pattern
class ScoreManager
{
    #pragma warning disable 0649

    //トータルスコア
    private int totalScore = 0;
    //スコアの増加量
    private int increasedRate = 1;
    //コンボ数
    private int combo = 0;
    
    // ----------------------- apply singleton -----------------------
    private static ScoreManager scoreInstance;
    private ScoreManager(){
    }

    public static ScoreManager GetScoreInstance(){
        if(scoreInstance == null){
            scoreInstance = new ScoreManager();
        }

        return scoreInstance;
    }

    // ----------------------- applied singleton -----------------------

    public int GetTotalScore(){
        return totalScore;
    }

    public int GetIncreasedRate(){
        return increasedRate;
    }

    public void SetIncreasedRate(int increasedRate){
        this.increasedRate = increasedRate;
    }

    public void IncrementCombo(){
        combo ++;
    }

    public void ResetCombo(){
        combo = 0;
    }

    public int ScaleByCombo(){
        int scale = 1;
        if(combo >= 0 && combo < 10){
            scale = 2;
        }else if(combo >= 10 && combo < 20){
            scale = 4;
        }else{
            scale = 8;
        }

        return scale;
    }

    public void IncreaseTotalScore(int score){
        totalScore += score * ScaleByCombo();
    }

    public static void DestroySingleton(){
        scoreInstance = null;
    }
}