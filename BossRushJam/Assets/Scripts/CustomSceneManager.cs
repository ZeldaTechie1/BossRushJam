using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject PauseScreen;
    public GameObject GameOverScreen;
    public Image GameOverBackdrop;
    public Image GameOverButton;
    public TMP_Text GameOverText;

    public void LoadNextScene(int index)
    {
        if(isPaused)
        {
            TogglePause();
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            if(PauseScreen != null)
            {
                PauseScreen.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 0;
            if (PauseScreen != null)
            {
                PauseScreen.SetActive(true);
            }
        }
        isPaused = !isPaused;
    }

    public void PlayerDied()
    {
        Time.timeScale = .05f;
        GameOverScreen.SetActive(true);
        GameOverBackdrop.DOColor(Color.black, 1f);
        GameOverButton.DOColor(Color.white, 1f);
        GameOverText.DOColor(Color.white, 1f);
    }
}
