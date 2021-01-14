using System;
using UnityEngine;

[System.Serializable]
public class ScoreJSONClass
{
    [SerializeField] private int score = 0;

    public int Score{
        get{return score;}
        set{score = value;}
    }
}