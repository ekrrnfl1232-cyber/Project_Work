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

    private void Awake()
    {
        inven = inventory.transform.GetChild(0).gameObject;
        equip = equipSystem.transform.GetChild(0).gameObject;
        quest = Quest.transform.GetChild(0).gameObject;
        OnLoad();
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
        if(InputManger.Instance.input.UI.Inventory.WasPressedThisFrame())
        {
            inven.SetActive(!inven.activeSelf);
            if (quest.activeInHierarchy)
            {
                quest.SetActive(!inven.activeSelf);
            }
            IsInventory(inven.activeSelf);
        }
        if(InputManger.Instance.input.UI.Quest.WasPressedThisFrame())
        {
            quest.SetActive(!quest.activeSelf);
        }
        if(InputManger.Instance.input.UI.Equip.WasPressedThisFrame())
        {
            Debug.Log(equip.activeSelf);    
            equip.SetActive(!equip.activeSelf);
        }
        
    }

    private void IsInventory(bool active)
    {
        if(active)
        {
            InputManger.Instance.input.Player.Disable();
        }
        else
        {
            InputManger.Instance.input.Player.Enable();
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
        PlayerProgress.Instance.AddExp(data.exp);
        PlayerProgress.Instance.AddGold(data.gold);

        inventory.LoadInventory(data.invendata);
        //equipSystem.LoadEquip(data.equipDatas);
    }
}
