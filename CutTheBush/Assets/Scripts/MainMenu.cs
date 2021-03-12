using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;

    public string levelToLoad = "MainLevel";

    AudioManager am;

    public string mainMenuMelody = "MainMenuMelody";

    public string buttonPressSound = "ButtonPress";

    private void Start()
    {
        am = AudioManager.instance;
        am.PlaySound(mainMenuMelody);
    }

    public void Play()
    {
        am.PlaySound(buttonPressSound);
        sceneFader.FadeTo(levelToLoad);
        am.StopSound(mainMenuMelody);
    }
}
