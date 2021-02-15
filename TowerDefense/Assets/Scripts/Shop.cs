using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    public Text standartTurretCost;
    public Text missileLauncherCost;
    public Text laserBeamerCost;

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
        standartTurretCost.text = "$" + standartTurret.cost.ToString();
        missileLauncherCost.text = "$" + missileLauncher.cost.ToString();
        laserBeamerCost.text = "$" + laserBeamer.cost.ToString();
    }
    public void SelectStandartTurret()
    {
        //Debug.Log("Standart turret Selected");
        buildManager.SelectTurretToBuild(standartTurret);
    }
    public void SelectMissileTurret()
    {
        //Debug.Log("Missile turret Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }
    public void SelectLaserBeamer()
    {
        //Debug.Log("Laser beamer Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
