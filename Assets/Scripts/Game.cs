using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _totalEnemy;

    public static event Action OnWin;
    public static event Action OnLose;

    public static bool GameRunning = true;

    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        ScoreController.SetTotalEnemy(_totalEnemy);
        ScoreController.SetDefaultScore();

        ScoreController.OnScoreChanged += WinCheck;
        Playership.OnPlayerKilled += Lose;

        Time.timeScale = 1;
        GameRunning = true;
    }

    private void WinCheck(int score)
    {
        if (score >= ScoreController.TotalEnemy)
            Win();
    }

    private void Win()
    {
        GameOver();
        OnWin?.Invoke();
    }

    private void Lose()
    {
        GameOver();
        OnLose?.Invoke();
    }

    private void GameOver()
    {
        GameRunning = false;
        Time.timeScale = 0;

        ScoreController.SaveHighscore();
    }
}
