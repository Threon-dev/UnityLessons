using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    string mouseHoverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in Weapon script");
        }
    }
    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);
        Application.Quit();
        Debug.Log("Quit the game");
    }
    public void Restart()
    {
        audioManager.PlaySound(buttonPressSound); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       
    }
    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }   
}
