using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        GameEvents.onReadytoLoadScene?.Invoke(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        GameEvents.onReadytoLoadScene?.Invoke(0);
    }
}
