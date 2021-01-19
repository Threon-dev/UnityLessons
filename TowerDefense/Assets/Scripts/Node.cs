using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Vector3 offSetPoint;
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;

    private GameObject turret;

    BuildManager buildManager;
    
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build there!-TODO Display on screen");
            return;
        }
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret=(GameObject)Instantiate(turretToBuild, transform.position+offSetPoint, transform.rotation);
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
