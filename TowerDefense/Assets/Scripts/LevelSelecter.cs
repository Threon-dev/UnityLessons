using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelButtons;

    AudioManager audioManager;

    public string hoverSound = "HoverSound";
    public string clickSound = "ClickSound";
    public string introSound = "Intro";

    private void Start()
    {
        audioManager = AudioManager.instance;

        audioManager.PlaySound(introSound);

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReached)
                levelButtons[i].interactable = false; 
        }
    }

    public void Select(string levelName)
    {
        audioManager.PlaySound(clickSound);
        fader.FadeTo(levelName);
        audioManager.StopSound(introSound);
    }

    public void OnMouseEnter()
    {
        audioManager.PlaySound(hoverSound);
    }
}
