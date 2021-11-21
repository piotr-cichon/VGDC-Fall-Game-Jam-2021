using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWon : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}