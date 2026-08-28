using UnityEngine;
using UnityEngine.UI;

public class UIConstroller : Singleton<UIConstroller>
{
    public bool isOnInventory = false;

    public Inventory inventory;
    public EquipSystem equipSystem;
    public InventoryItem moveItem;

    void Start()
    {
        moveItem.gameObject.SetActive(false);
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
        Image Iven = inventory.gameObject.GetComponent<Image>();
        GameObject Inven = inventory.gameObject.transform.GetChild(0).gameObject;
        Color alphaI = Iven.color;
        if (Inven.activeInHierarchy)
        {
            alphaI.a = 0;
            Inven.SetActive(false);
            isOnInventory = false;
        }
        else
        {
            alphaI.a = 0.6f;
            Inven.SetActive(true);
            isOnInventory = true;
        }
        Iven.color = alphaI;
    }

}
