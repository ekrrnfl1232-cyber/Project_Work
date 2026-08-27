using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image iconImg;
    
    [SerializeField]
    private TMP_Text nameTxt;

    [SerializeField]
    private Image equipImg;

    [SerializeField]
    private TMP_Text countTxt;

    private uint count;

    private ItemScriptable data;
    // ∞¯¿Ø ∫Øºˆ
    public ItemScriptable Data => data;
    public uint Count => count;
    public InventoryItem Init(ItemScriptable data)
    {
        this.data = data;
        return this;
    }

    public void Setting()
    {
        iconImg.sprite = data.Icon;
        nameTxt.text = data.ItemName;
        equipImg.gameObject.SetActive(false);
        if (data.Type == ItemType.Equip)
        {
            countTxt.gameObject.SetActive(false);
        }
        else
        {
            SetCount(1);
        }
    }

    public void SetCount(uint cnt)
    {
        if (countTxt.IsActive())
        {
            count += cnt;
            if (data.Type == ItemType.Gold)
            {
                countTxt.text = $"{count}";
            }
            else if(data.Type == ItemType.Posion)
            {
                countTxt.text = $"{count} / {data.MaxStack}";
            }
        }
        else
            return;
    }

    public void OnUse()
    {
        
        if (data.Type == ItemType.Equip)
        {
            if (equipImg.IsActive())
            {
                equipImg.gameObject.SetActive(false);
                Debug.Log($"{data.ItemName} ¿Â¬¯ «ÿ¡¶");
            }
            else
            {
                equipImg.gameObject.SetActive(true);
                Debug.Log($"{data.ItemName} ¿Â¬¯");
            }
        }
        else
        {
            count -= 1;
            SetCount(0);
            Debug.Log($"{data.ItemName}");
            if(count == 0)
            {
                OnDelete();
            }
            
        }
        
    }
    
    public void OnDelete()
    {
        Destroy(gameObject);
    }
}

