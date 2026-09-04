using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private InventoryItem invenItem;

    [SerializeField] private ItemScriptable[] itemDatas;

    [SerializeField] private TMP_Text goldAmount;

    public int gold { get; set; }

    private List<InventoryItem> items = new();
    private Image background;
    public Transform Inven => parent;

    private void Start()
    {
        itemDatas = Resources.LoadAll<ItemScriptable>("ItemData");
        background = parent.GetComponent<Image>();
        CreateItem(itemDatas[0], 1);
        CreateItem(itemDatas[1], 1);
        CreateItem(itemDatas[2], 1);
        CreateItem(itemDatas[4], 1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            int rand = Random.Range(0, itemDatas.Length);
            CreateItem(itemDatas[rand], 1);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnGoldChange += GoldAmount;
    }

    private void OnDisable()
    {
        GameEvents.OnGoldChange -= GoldAmount;
    }

    // 매개변수 int 추가
    // 반환 자료형 int  변경
    public int CreateItem(ItemScriptable item, uint count)
    {
        items.RemoveAll(item => item == null);
        if (items.Count != 0)
        {
            foreach (var i in items)
            {
                if (i.Data == item)
                {
                    if (i.Amount < item.MaxStack)
                    {
                        i.SetCount((uint)count);
                        return 0;
                    }
                }
            }
        }
        
        InventoryItem createItem = Instantiate(invenItem, parent);
        createItem.Init(item);
        createItem.Setting();
        items.Add(createItem);
        return 1;
    }

    public InvenData[] GetInvenDatas()
    {
        InvenData[] data = new InvenData[items.Count];
        for(int i = 0; i < items.Count; ++i)
        {
            if (items == null)
            {
                data[i] = new InvenData
                {
                    id = 1234,
                    stack = 0
                };
                continue;
            }
            data[i] = new InvenData
            {
                id = items[i].Data.ItemID,
                stack = items[i].Amount
            };
        }

        return data;
    }

    public void GoldAmount()
    {
        goldAmount.text = $"{PlayerProgress.Instance.Gold}";
        gold = PlayerProgress.Instance.Gold;
    }

    public void LoadInventory(InvenData[] data)
    {
        
    }
}
