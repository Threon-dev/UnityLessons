using UnityEngine;

public class DeviceTrigger2 : MonoBehaviour
{
    public bool requireKey;

    [SerializeField] 
    private GameObject[] targets;
    private void OnTriggerEnter(Collider other)
    {
        if(requireKey&& Managers.Inventory.equippedItem != "Key")
        {            
            return;
        }

        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}
