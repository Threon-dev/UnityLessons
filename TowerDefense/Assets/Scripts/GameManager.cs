using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver; 

    public GameObject gameOverUI;

    public GameObject completeLevelUI;

    private void Start()
    {
        gameIsOver = false;
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
    }
    
    public void WinLevel() 
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
