 using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string hoverOverSound = "ButtonHover";
    [SerializeField]
    string pressButtonSound = "ButtonPress";
    AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in MENU");
        }
    }
    public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Application.Quit();
        Debug.Log("We quit the game");
    }
    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
  
}
