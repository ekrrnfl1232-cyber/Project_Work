using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private InventoryItem invenItem;

    [SerializeField] private ItemScriptable[] itemDatas;

    private List<InventoryItem> items = new();
    public Transform Inven => parent;

    private void Start()
    {
        itemDatas = Resources.LoadAll<ItemScriptable>("ItemData");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            int rand = Random.Range(0, itemDatas.Length);
            CreateItem(itemDatas[rand]);
        }
    }

    public void CreateItem(ItemScriptable item)
    {
        items.RemoveAll(item => item == null);
        if (items.Count != 0)
        {
            foreach (var i in items)
            {
                if (i.Data == item)
                {
                    if (i.Count < item.MaxStack)
                    {
                        i.SetCount(1);
                        return;
                    }
                }
            }
        }
        InventoryItem createItem = Instantiate(invenItem, parent);
        createItem.Init(item);
        createItem.Setting();

        items.Add(createItem);
    }
}
