using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerUpHandler, IDragHandler, IBeginDragHandler
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

    private RectTransform moveItemRectTran;

    // 공유 변수
    public Image EquipImg { get { return equipImg; } set { equipImg = value; } }
    public Image IconImg { get; set; }
    public ItemScriptable Data { get; set; }
    public uint Amount => count;
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
        EquipImg.gameObject.SetActive(false);
        if(Data.itemType != ItemType.Equip)
            countTxt.gameObject.SetActive(true);
    }

    public void SetCount(uint cnt)
    {
        if(Data.itemType == ItemType.Equip)
        {
            count = 1;
        }
        else
        {
            count += cnt;
            countTxt.text = $"{Amount} / {Data.MaxStack}";
        }
    }

    public void OnUse()
    {
        if(Data.itemType != ItemType.Equip)
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

    public void OnPointerUp(PointerEventData eventData)
    {
        if(UIConstroller.Instance.equipSystem.SelectSlot != null)
        {
            UIConstroller.Instance.equipSystem.SelectSlot.Equip();
            EquipImg.gameObject.SetActive(!EquipImg.gameObject.activeSelf);
            Debug.Log($"{Data.ItemName} 장착");
        }
        UIConstroller.Instance.moveItem.gameObject.SetActive(false);
        moveItemRectTran = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveItemRectTran.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.Data = Data;

        moveItemRectTran = UIConstroller.Instance.moveItem.GetComponent<RectTransform>();
        moveItemRectTran.position = eventData.position;
        UIConstroller.Instance.moveItem.gameObject.SetActive(true);

        UIConstroller.Instance.moveItem.Setting();
    }
}

