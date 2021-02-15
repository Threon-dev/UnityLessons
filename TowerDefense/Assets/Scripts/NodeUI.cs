using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCostText;
    public Button upgradeButton;

    public Text sellCostText;

    private Node target;

    TurretBlueprint turretBlueprint;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (target.isUpgraded==0)
        {
            upgradeCostText.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }

        if (target.isUpgraded == 1)
        {
            upgradeCostText.text = "$" + target.turretBlueprint.secondUpgradeCost;
            upgradeButton.interactable = true;
        }

        if(target.isUpgraded == 2)
        {
            upgradeCostText.text = "DONE";
            upgradeButton.interactable = false;
        }
        sellCostText.text ="$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);       
    }

    public void Hide()
    {
        ui.SetActive(false); 
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode(); 
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
