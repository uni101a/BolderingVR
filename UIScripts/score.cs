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
        scoreText = GetComponent<Text>();
        scoreManager = ScoreManager.GetScoreInstance();
    }
}