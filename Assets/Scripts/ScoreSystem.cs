using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    public int currentScore;
    public int highScore;

    public TextMeshProUGUI inGameScore;
    public TextMeshProUGUI gameOverCS;
    public TextMeshProUGUI gameOverHS;
    public TextMeshProUGUI nextLevelCS;
    public TextMeshProUGUI nextLevelHS;
    
    
    private void Awake()
    {
        if (instance == null)
            instance = this;

        highScore = SaveManager.instance.GetHighScore();
    }

    private void Start()
    {
        ScoreCarryOver();
    }

    private void Update()
    {
        if (currentScore >= highScore)
        {
            SaveManager.instance.SetHighScore(currentScore);
            highScore = currentScore;
        }
        inGameScore.text = $"<size=60>{currentScore}</size><size=70>/</size><size=40><b>{highScore}</b></size>";
        gameOverCS.text = nextLevelCS.text = currentScore.ToString();
        gameOverHS.text = nextLevelHS.text = highScore.ToString();
    }

    public void ScoreCarryOver()
    {
        if (SaveManager.instance.GetHasDiedPreviousLevel())
        {
            currentScore = 0;
            SaveManager.instance.SetCurrentScoreFromPreviousLevel(currentScore);
        }
        else
        {
            currentScore = SaveManager.instance.GetCurrentScoreFromPreviousLevel();
        }
    }
}
