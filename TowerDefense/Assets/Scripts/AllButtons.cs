using UnityEngine.SceneManagement;
using UnityEngine;

public class AllButtons : MonoBehaviour
{
    public SceneFader sceneFader;

    AudioManager audioManager;

    [Header("PauseMenu")]
    public GameObject pauseMenu;

    [Header("CompleteLevelOptions")]
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;


    public string hoverSound = "HoverSound";
    public string clickSound = "ClickSound";
    public string levelMelody = "LevelMusic";
    private void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlaySound(levelMelody);
    }


    //GameOver UI
    public void Retry()
    {
        audioManager.PlaySound(clickSound);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        audioManager.PlaySound(clickSound);
        sceneFader.FadeTo(sceneFader.menuSceneName);
        audioManager.StopSound(levelMelody);
    }

    //PauseMenu UI
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void BackToMenu()
    {
        audioManager.PlaySound(clickSound);
        Toggle();
        sceneFader.FadeTo(sceneFader.menuSceneName);
        audioManager.StopSound(levelMelody);
    }
    public void RetryPause()
    {
        audioManager.PlaySound(clickSound);
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    //Complete level UI
    public void Continue()
    {
        audioManager.PlaySound(clickSound);
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
        audioManager.StopSound(levelMelody);
    }

    public void OnMouseEnter()
    {
        audioManager.PlaySound(hoverSound);
    }
}
