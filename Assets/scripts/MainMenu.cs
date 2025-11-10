using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called by Start button
    public void PlayGame()
    {
        // Make sure this matches your gameplay scene name exactly
        SceneManager.LoadScene("GameScene");
    }

    // Called by Quit button
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // works in editor
    }
}
