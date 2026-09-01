using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private InventoryItem invenItem;

    [SerializeField] private ItemScriptable[] itemDatas;

    [SerializeField] private TMP_Text goldAmount;

    private List<InventoryItem> items = new();
    private Image background;
    public Transform Inven => parent;

    private void Start()
    {
        itemDatas = Resources.LoadAll<ItemScriptable>("ItemData");
        background = parent.GetComponent<Image>();
        PlayerProgress.Instance.OnChanged += GoldAmount;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            int rand = Random.Range(0, itemDatas.Length);
            CreateItem(itemDatas[rand], 1);
        }
    }

    // 매개변수 int 추가
    // 반환 자료형 int  변경
    public int CreateItem(ItemScriptable item, int count)
    {
        items.RemoveAll(item => item == null);
        if (items.Count != 0)
        {
            foreach (var i in items)
            {
                if (i.Data == item && i.Data.Type != ItemType.Equip)
                {
                    if (i.Count < item.MaxStack)
                    {
                        i.SetCount(1);
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

    public void GoldAmount()
    {
        goldAmount.text = $"{PlayerProgress.Instance.Gold}";
    }
}
