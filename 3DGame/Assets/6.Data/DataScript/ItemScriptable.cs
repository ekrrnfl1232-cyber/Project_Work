using System;
using UnityEngine;

public enum ItemType
{
    Equip,
    Posion,
    Gold
}

[CreateAssetMenu]
public class ItemScriptable : ScriptableObject
{
    [SerializeField]
    private int itemID;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private uint maxStack;

    [SerializeField]
    private int price;

    public int ItemID => itemID;
    public ItemType Type;
    public string ItemName => itemName;
    public Sprite Icon => icon;
    public uint MaxStack => maxStack;
    public int Price => price;
}
