using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Vector3 offSetPoint;
    public Color hoverColor;
    private Color startColor;
    public Color notEnoughMoney;
    private Renderer rend;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public int isUpgraded = 0; 

    BuildManager buildManager;

    AudioManager audioManager;
    public string buildingComplete = "BuildingComplete";
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in scene");
        }
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + offSetPoint;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this );
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject buildEffectObject = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffectObject, 2f);

        //Debug.Log("Turret build");

        audioManager.PlaySound(buildingComplete);
    }

    public void UpgradeTurret()
    {
        if(isUpgraded == 0)
        {
            if (PlayerStats.Money < turretBlueprint.upgradeCost)
            {
                Debug.Log("Not enough money to upgrade that");
                return;
            }

            PlayerStats.Money -= turretBlueprint.upgradeCost;

            //Get rid of the old turret
            Destroy(turret);

            //Building a new one
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            GameObject buildEffectObject = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(buildEffectObject, 2f);

            isUpgraded = 1;

            Debug.Log("Turret upgraded!");

            return;
        }

        if (isUpgraded == 1)
        {
            if (PlayerStats.Money < turretBlueprint.secondUpgradeCost)
            {
                Debug.Log("Not enough money to upgrade that");
                return;
            }

            PlayerStats.Money -= turretBlueprint.secondUpgradeCost;

            //Get rid of the old turret
            Destroy(turret);

            //Building a new one
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.secondUpgradePrefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            GameObject buildEffectObject = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(buildEffectObject, 2f);

            isUpgraded = 2;

            Debug.Log("Turret upgraded! x2");
        }

    } 
    
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject buildEffectObject = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffectObject, 2f);

        Destroy(turret);
        turretBlueprint = null;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
             rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoney;
        }

       
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
