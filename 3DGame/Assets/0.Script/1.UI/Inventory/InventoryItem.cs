using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField]
    private Image iconImg;
    
    [SerializeField]
    private TMP_Text nameTxt;

    [SerializeField]
    private Image equipImg;

    [SerializeField]
    private TMP_Text countTxt;

    [SerializeField]
    private Image back;

    private uint count;

    private InventoryItem moveItem;
    private RectTransform moveItemRectTran;

    // 공유 변수
    public Image EquipImg { get; set; }
    public Image IconImg { get; set; }
    public ItemScriptable Data { get; set; }
    public uint Count => count;
    public InventoryItem Init(ItemScriptable data)
    {
        this.Data = data;
        back = GetComponent<Image>();
        return this;
    }

    public void Setting()
    {
        back.sprite = Data.BackgroundIcon;
        iconImg.sprite = Data.Icon;
        nameTxt.text = Data.ItemName;
        equipImg.gameObject.SetActive(false);
        if (Data.Type == ItemType.Equip)
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
        count += cnt;
        if (Data.Type == ItemType.Gold)
        {
            countTxt.text = $"{count}";
        }
        else if(Data.Type == ItemType.Posion)
        {
            countTxt.text = $"{count} / {Data.MaxStack}";
        }
    }

    public void OnUse()
    {
        if(Data.Type != ItemType.Equip)
        {
            count -= 1;
            SetCount(0);
            Debug.Log($"{Data.ItemName}");
            if(count == 0)
            {
                OnDelete();
            }
            
        }
        else
        {
            return;
        }
    }
    public void OnDelete()
    {
        Destroy(gameObject);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(UIConstroller.Instance.equipSystem.SelectSlot != null)
        {
            UIConstroller.Instance.equipSystem.SelectSlot.Equip();
            if (!equipImg.IsActive())
            {
                equipImg.gameObject.SetActive(true);
                Debug.Log($"{Data.ItemName} 장착");
            }
        }
        moveItem.gameObject.SetActive(false);
        moveItemRectTran = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveItemRectTran.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.Data = Data;

        this.moveItem = UIConstroller.Instance.moveItem;

        moveItemRectTran = this.moveItem.GetComponent<RectTransform>();
        moveItemRectTran.position = eventData.position;
        this.moveItem.gameObject.SetActive(true);

        moveItem.Setting();
    }
}

