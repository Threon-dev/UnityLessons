using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver; 

    public GameObject gameOverUI;

    public GameObject completeLevelUI;

    AudioManager audioManager;

    public string endGameSound = "GameOver";

    private void Start()
    {
        gameIsOver = false;
        audioManager = AudioManager.instance;
    }
    void Update()
    {
        if (gameIsOver)
            return;
           
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
        audioManager.PlaySound(endGameSound);
    }
    
    public void WinLevel() 
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
