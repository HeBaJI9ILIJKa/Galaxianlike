using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private GameObject[] _screens;

    public void SelectActiveScreen(string screenName)
    {
        foreach (var screen in _screens)
        {
            if(screen.name == screenName)
                screen.SetActive(true);
            else
                screen.SetActive(false);
        }
    }
}
