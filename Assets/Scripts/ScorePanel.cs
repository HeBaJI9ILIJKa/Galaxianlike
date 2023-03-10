using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalEnemyAmount;
    [SerializeField] private TextMeshProUGUI _highscoreAmount;
    [SerializeField] private TextMeshProUGUI _scoreAmount;

    private void OnEnable()
    {
        ScoreController.OnScoreChanged += UpdateScoreAmount;
        ScoreController.OnHighscoreChanged += UpdateHighscoreAmount;
        ScoreController.OnTotalEnemyChanged += UpdateTotalEnemyAmount;
    }

    private void OnDisable()
    {
        ScoreController.OnScoreChanged -= UpdateScoreAmount;
        ScoreController.OnHighscoreChanged -= UpdateHighscoreAmount;
        ScoreController.OnTotalEnemyChanged -= UpdateTotalEnemyAmount;
    }

    private void UpdateTotalEnemyAmount(int value)
    {
        _totalEnemyAmount.text = value.ToString();
    }
    private void UpdateHighscoreAmount(int value)
    {
        _highscoreAmount.text = value.ToString();
    }
    private void UpdateScoreAmount(int value)
    {
        _scoreAmount.text = value.ToString();
    }

}
