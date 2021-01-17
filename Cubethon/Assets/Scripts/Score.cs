using UnityEngine.UI;
using UnityEngine;
public class Score : MonoBehaviour
{
    public Transform player;
    public Text ScoreText;
    void Update()
    {
       ScoreText.text = player.position.z.ToString("0");       
    }
}
