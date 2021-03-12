using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;

    public string nextLevelToLoad = "MainLevel";

    AudioManager am;

    public string buttonPressSound = "ButtonPress";
    private void Start()
    {
        am = AudioManager.instance;
    }
    public void LoadNextLevel()
    {
        am.PlaySound(buttonPressSound);
        sceneFader.FadeTo(nextLevelToLoad);
    }
}
