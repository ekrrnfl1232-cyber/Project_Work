using TMPro;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject txtBGObj;
    [SerializeField]
    private Image iconImg;
    [SerializeField]
    private TMP_Text itemNameTxt;
    [SerializeField]
    private EquipType type;

    private bool isExit;

    public ItemScriptable Data { get; set; }
    private InventoryItem moveItem;
    private RectTransform moveItemRectTran;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIConstroller.Instance.moveItem.Data.itemType == ItemType.Equip)
        {
            if (UIConstroller.Instance.moveItem.Data.equipType == type)
            {
                UIConstroller.Instance.equipSystem.SelectSlot = this;
            }
            else
            {
                Debug.Log($"{type}만 가능합니다");
                return;
            }
        }
        else
        {
            Debug.Log("장비만 가능합니다.");
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIConstroller.Instance.equipSystem.SelectSlot = null;

        moveItem = null;
    }
    public bool IsInPoint(Vector2 postion)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(iconImg.rectTransform, postion);
    }

    public void Equip()
    {
        this.moveItem = UIConstroller.Instance.moveItem;
        Data = UIConstroller.Instance.moveItem.Data;

        iconImg.sprite = moveItem.Data.Icon;
        itemNameTxt.text = moveItem.Data.ItemName;
        iconImg.gameObject.SetActive(true);
        txtBGObj.SetActive(true);
    }
    public void UnEquip()
    {
        UIConstroller.Instance.moveItem.EquipImg.gameObject.SetActive(false);
        UIConstroller.Instance.equipSystem.SelectSlot = null;
        iconImg.gameObject.SetActive(false);
        txtBGObj.SetActive(false);
        moveItem = null;
        Data = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        moveItemRectTran.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.Data = Data;

        this.moveItem = UIConstroller.Instance.moveItem;

        moveItemRectTran = UIConstroller.Instance.moveItem.GetComponent<RectTransform>();
        moveItemRectTran.position = eventData.position;
        UIConstroller.Instance.moveItem.gameObject.SetActive(true);

        UIConstroller.Instance.moveItem.Setting();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.gameObject.SetActive(false);
        if (!IsInPoint(eventData.position))
        {
            UnEquip();
        }
    }
}
