using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreUpdate : MonoBehaviour
{
    private Text scoreText;

    private Text highscoreText;

    private ScoreManager scoreManager;

    private ScoreJSONClass scoreJSONClass;

    private string _datapath;

    void Start()
    {
        scoreText = transform.GetChild(0).GetComponent<Text>();
        highscoreText = transform.GetChild(1).GetComponent<Text>();
        scoreManager = ScoreManager.GetScoreInstance();
        scoreJSONClass = new ScoreJSONClass();
        _datapath = Application.dataPath + "/Scripts/UIScripts/score.json";

        scoreText.text = "SCORE : " + scoreManager.GetTotalScore().ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore(){
        int highscore = GetHighScoreData();

        if(highscore < scoreManager.GetTotalScore()){
            int score = scoreManager.GetTotalScore();
            scoreText.color = Color.red;
            HighScore(score);
            SaveScore(score);
        }else{
            HighScore(highscore);
        }
    }

    private void HighScore(int score){
        highscoreText.text = "HIGH SCORE : " + score.ToString();
    }

    private int GetHighScoreData(){
        if (!File.Exists(_datapath)) return 0;

        var json = File.ReadAllText(_datapath);

        var obj = JsonUtility.FromJson<ScoreJSONClass>(json);
        return obj.Score;
    }

    private void SaveScore(int score){
        ScoreJSONClass jsonClass = scoreJSONClass;
        jsonClass.Score = score;
        var json = JsonUtility.ToJson(jsonClass, prettyPrint:true);
        File.WriteAllText(_datapath, json);
    }
}