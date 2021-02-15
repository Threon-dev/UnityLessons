using UnityEngine;

[System.Serializable]
public class TurretBlueprint 
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public GameObject secondUpgradePrefab;
    public int secondUpgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;  
    }
}
