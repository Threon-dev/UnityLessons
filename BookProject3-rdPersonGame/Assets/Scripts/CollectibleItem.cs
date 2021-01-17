using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string ItemName;
    private void OnTriggerEnter(Collider other)
    {
        Managers.Inventory.AddItem(ItemName);
        Destroy(this.gameObject);
    }
}
