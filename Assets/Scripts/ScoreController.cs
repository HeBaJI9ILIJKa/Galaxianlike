using System;
using UnityEngine;

public static class ScoreController
{
    private static int _score = 0;
    private static int _highscore = 0;
    private static int _totalEnemy = 50;

    public static int TotalEnemy => _totalEnemy;
    public static int Score => _score;
    public static int Highscore => _highscore;

    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighscoreChanged;
    public static event Action<int> OnTotalEnemyChanged;

    public static void SetTotalEnemy(int value)
    {
        _totalEnemy = value;
    }

    public static void SetDefaultScore()
    {
        _highscore = PlayerPrefs.GetInt("highscore", 0);
        _score = 0;

        OnScoreChanged?.Invoke(_score);
        OnHighscoreChanged?.Invoke(_highscore);
        OnTotalEnemyChanged?.Invoke(TotalEnemy);
    }

    public static void ScoreIncrease()
    {
        _score++;
        OnScoreChanged?.Invoke(_score);
    }
        
    public static void SaveHighscore()
    {
        if (_score > _highscore)
            PlayerPrefs.SetInt("highscore", _score);
    }
}
