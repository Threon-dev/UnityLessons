using UnityEngine.UI;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text speedText;
    [SerializeField]
    private float healthMultiplier = 1.2f;

    [SerializeField]
    private float speedMultiplier = 1.2f;
    [SerializeField]
    private int upgradeCost = 50;
    private PlayerStats stats;

    
    void OnEnable()
    {
        stats = PlayerStats.instance;
        UpdateValues();
    }
    void UpdateValues()
    {
        healthText.text = "HEALTH: "+stats.maxHealth.ToString();
        speedText.text = "SPEED: "+stats.speed.ToString();
    }
    public void UpgradeHealth()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.maxHealth = (int)(stats.maxHealth*healthMultiplier);

        GameMaster.Money -= upgradeCost;

        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }
    public void UpgradeSpeed()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.speed = Mathf.Round(stats.speed * speedMultiplier);

        GameMaster.Money -= upgradeCost;

        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }
}
