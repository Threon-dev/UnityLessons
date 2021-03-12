using UnityEngine;
using TMPro;

public class ScoreLabel : MonoBehaviour
{
    public TextMeshProUGUI scoreLabel;

    public static int _scorePoints = 0;

    private void Start()
    {       
        scoreLabel.text = _scorePoints.ToString();
    }

    private void Update()
    {
        scoreLabel.text = _scorePoints.ToString();
    }
}
