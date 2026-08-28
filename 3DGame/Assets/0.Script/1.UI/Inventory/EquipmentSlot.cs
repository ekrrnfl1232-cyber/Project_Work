using TMPro;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerUpHandler,IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject txtBGObj;
    [SerializeField]
    private Image iconImg;
    [SerializeField]
    private TMP_Text itemNameTxt;

    private bool isExit;

    public ItemScriptable Data { get; set; }
    private InventoryItem moveItem;
    private RectTransform moveItemRectTran;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIConstroller.Instance.moveItem.Data.Type == ItemType.Equip)
        {
            UIConstroller.Instance.equipSystem.SelectSlot = this;
        }
        else
        {
            Debug.Log("장비만 가능합니다.");
            return;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.EquipImg.gameObject.SetActive(false);
        Debug.Log($"{UIConstroller.Instance.moveItem.Data.ItemName} 장착해제");
        UIConstroller.Instance.moveItem.gameObject.SetActive(false);
        moveItem = null;
        moveItemRectTran = null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIConstroller.Instance.equipSystem.SelectSlot = null;

        moveItem = null;
    }
    public void Equip()
    {
        this.moveItem = UIConstroller.Instance.moveItem;
        Data = moveItem.Data;

        iconImg.sprite = moveItem.Data.Icon;
        itemNameTxt.text = moveItem.Data.ItemName;
        iconImg.gameObject.SetActive(true);
        txtBGObj.SetActive(true);
    }
    public void UnEquip()
    {
        iconImg.gameObject.SetActive(false);
        txtBGObj.SetActive(false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        moveItemRectTran.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.Data = Data;

        moveItem = UIConstroller.Instance.moveItem;

        moveItemRectTran = moveItem.GetComponent<RectTransform>();
        moveItemRectTran.position = eventData.position;
        moveItem.gameObject.SetActive(true);

        moveItem.Setting();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UnEquip();
    }
}
