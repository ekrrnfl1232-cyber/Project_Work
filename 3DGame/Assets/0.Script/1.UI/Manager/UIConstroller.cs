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

    public PlayerStat playerStat;

    private GameObject inven;
    private GameObject equip;
    private GameObject quest;

    private InputAction invenKey;
    private InputAction questKey;

    private void Awake()
    {
        OnLoad();
        inven = inventory.transform.GetChild(0).gameObject;
        equip = equipSystem.transform.GetChild(0).gameObject;
        quest = Quest.transform.GetChild(0).gameObject;
        invenKey = InputSystem.actions.FindAction("Inventory");
        questKey = InputSystem.actions.FindAction("Quest");
    }

    void Start()
    {
        moveItem.gameObject.SetActive(false);
        quest.SetActive(false);
        inven.SetActive(false);
        equip.SetActive(false);
    }
    void Update()
    {
        if(invenKey.WasPressedThisFrame())
        {
            inven.SetActive(!inven.activeSelf);
            equip.SetActive(!equip.activeSelf);
            quest.SetActive(!inven.activeSelf);
            GameEvents.RaiseInven(inven.activeSelf);
        }
        if(questKey.WasPressedThisFrame())
        {
            quest.SetActive(!quest.activeSelf);
        }
        
    }

    public void OnSave()
    {
        GameSaveData data = new GameSaveData();

        data.level = playerStat.Level;
        data.Hp = playerStat.Hp;
        data.exp = playerStat.Exp;
        data.gold = inventory.gold;
        data.invendata = inventory.GetInvenDatas();
        data.equipDatas = equipSystem.GetEquipData();

        SaveManager.Instance.Save(data);
    }
    public void OnLoad()
    {
        GameSaveData data = SaveManager.Instance.Load();
        playerStat.Level = data.level;
        playerStat.Hp = data.Hp;
        playerStat.Exp = data.exp;
        inventory.gold = data.gold;

        inventory.LoadInventory(data.invendata);
        equipSystem.LoadEquip(data.equipDatas);
    }
}
