using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;

    private bool isActive = false;

    public GameObject gameOverUI;

    private void Start()
    {
        gameIsOver = false;
    }
    void Update()
    {
        if (gameIsOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape)&&isActive==false)
        {
            gameOverUI.SetActive(true);
            isActive = true;
        }
        else if(isActive == true && gameIsOver == false && Input.GetKeyDown(KeyCode.Escape))
        {
            gameOverUI.SetActive(!true);
            isActive = false;                    
        }
       
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
}
