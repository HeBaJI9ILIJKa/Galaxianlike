using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private ScreenController _screenController;

    private void OnEnable()
    {
        Game.OnWin += ActivateWinScreen;
        Game.OnLose += ActivateLoseScreen;
    }
    private void OnDisable()
    {
        Game.OnWin -= ActivateWinScreen;
        Game.OnLose -= ActivateLoseScreen;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();
    }

    public void ActivateWinScreen()
    {
        _screenController.SelectActiveScreen("WinScreen");
    }

    public void ActivateLoseScreen()
    {
        _screenController.SelectActiveScreen("LoseScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
