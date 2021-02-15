using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;

    AudioManager audioManager;

    public string mouseHover = "HoverSound";
    public string mouseClick = "ClickSound";
    public string mainMenuMelody = "MainMenuMelody";

    private void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.PlaySound(mainMenuMelody);    
    }

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
        audioManager.PlaySound(mouseClick);
        audioManager.StopSound(mainMenuMelody);
    }
    public void Quit()
    {
        audioManager.PlaySound(mouseClick);
        audioManager.StopSound(mainMenuMelody);
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void OnMouseEnter()
    {
        audioManager.PlaySound(mouseHover);
    }
}
