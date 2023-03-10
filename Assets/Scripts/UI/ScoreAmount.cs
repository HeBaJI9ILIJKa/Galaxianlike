using UnityEngine;
using TMPro;

public class ScoreAmount : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = ScoreController.Score.ToString();
    }
}
