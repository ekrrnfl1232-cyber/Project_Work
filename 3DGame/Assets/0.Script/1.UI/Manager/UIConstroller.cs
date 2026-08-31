using UnityEngine;
using UnityEngine.UI;

public class UIConstroller : Singleton<UIConstroller>
{
    public bool isOnInventory;

    public Inventory inventory;
    public EquipSystem equipSystem;
    public InventoryItem moveItem;

    void Start()
    {
        moveItem.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        isOnInventory = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInventory();
        }
    }

    public void OnInventory()
    {
        if (inventory.gameObject.activeInHierarchy)
        {
            inventory.gameObject.SetActive(false);
            isOnInventory = false;
        }
        else
        {
            inventory.gameObject.SetActive(true);
            isOnInventory = true;
        }
    }

}
