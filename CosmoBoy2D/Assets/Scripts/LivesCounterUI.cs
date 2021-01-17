using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour
{  
    private Text livesText;
    private void Awake()
    {
        livesText = GetComponent<Text>();
    }
    private void Update()
    {
        if (GameMaster.RemainingLives >= 0)
        {
            livesText.text = "LIVES: " + GameMaster.RemainingLives.ToString();
        }
        else
        {
            livesText.text = "LIVES: " + 0;
        }
    }
}
