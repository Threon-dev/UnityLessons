using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour
{
    private Text moneyCount;
    private void Awake()
    {
        moneyCount = GetComponent<Text>();
    }
    private void Update()
    {
        if (GameMaster.RemainingLives >= 0)
        {
            moneyCount.text = "Money: " + GameMaster.Money.ToString();
        }     
    }
}
