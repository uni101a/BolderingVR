using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    private Text scoreText;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreText = transform.GetChild(0).GetComponent<Text>();
        scoreManager = ScoreManager.GetScoreInstance();
    }

    void Update()
    {
        scoreText.text = "Score : " + scoreManager.GetTotalScore();    
    }
}