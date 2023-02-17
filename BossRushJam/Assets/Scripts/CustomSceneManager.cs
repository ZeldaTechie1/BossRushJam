using DG.Tweening;
using System.Collections.Generic;
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
    public GameAudioEventManager gameAudioEventManager;

    List<Tweener> tweens = new List<Tweener>();

    public void LoadNextScene(int index)
    {
        if(isPaused)
        {
            TogglePause();
        }
        gameAudioEventManager.StopBGM();
        Time.timeScale = 1;

        Destroy(gameAudioEventManager);
        SceneManager.LoadScene(index);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        gameAudioEventManager.PauseBGM();
        if (isPaused)
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
        tweens.Add(GameOverBackdrop.DOColor(Color.black, 1f));
        tweens.Add(GameOverButton.DOColor(Color.white, 1f));
        tweens.Add(GameOverText.DOColor(Color.white, 1f));

        gameAudioEventManager.StopBGM();
    }

    public void GameBeat()
    {
        Time.timeScale = .1f;
        GameOverScreen.SetActive(true);
        GameOverButton.gameObject.SetActive(false);
        tweens.Add(GameOverBackdrop.DOColor(Color.black, .25f).OnComplete(()=>LoadNextScene(2)));
    }

    public void OnDestroy()
    {
        foreach(Tweener tween in tweens)
        {
            tween.Kill();
        }
    }
}
