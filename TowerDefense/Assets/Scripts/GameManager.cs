using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;
    void Update()
    {
        if (gameEnded)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameEnded = true;
        Debug.Log("GAME OVER");
    }
}
