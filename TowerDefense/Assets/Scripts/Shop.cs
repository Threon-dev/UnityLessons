using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandartTurret()
    {
        Debug.Log("Standart turret Selected");
        buildManager.SetTurretToBuild(buildManager.standartTurretPrefab);
    }
    public void PurchaseMissileTurret()
    {
        Debug.Log("Missile turret Selected");
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}
