using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIConstroller : Singleton<UIConstroller>
{
    public bool isOnInventory;

    public Inventory inventory;
    public EquipSystem equipSystem;
    public InventoryItem moveItem;

    public QuestUi Quest;

    private GameObject inven;
    private GameObject quest;

    private InputAction invenKey;
    private InputAction questKey;

    private void Awake()
    {
        inven = inventory.transform.GetChild(0).gameObject;
        quest = Quest.transform.GetChild(0).gameObject;
        invenKey = InputSystem.actions.FindAction("Inventory");
        questKey = InputSystem.actions.FindAction("Quest");
    }

    void Start()
    {
        moveItem.gameObject.SetActive(false);
        quest.SetActive(false);
        inven.SetActive(false);
    }
    void Update()
    {
        if(invenKey.WasPressedThisFrame())
        {
            inven.SetActive(!inven.activeSelf);
            quest.SetActive(!inven.activeSelf);
            GameEvents.RaiseInven(inven.activeSelf);
        }
        if(questKey.WasPressedThisFrame())
        {
            quest.SetActive(!quest.activeSelf);
        }
        
    }
}
