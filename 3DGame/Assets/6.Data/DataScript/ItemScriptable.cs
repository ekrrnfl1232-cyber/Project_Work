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
    private Sprite backgroundIcon;

    [SerializeField]
    private uint maxStack;

    [SerializeField]
    private int price;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int defence;

    [SerializeField]
    private float speed;

    public int ItemID => itemID;
    public ItemType Type;
    public string ItemName => itemName;
    public Sprite Icon => icon;
    public Sprite BackgroundIcon => backgroundIcon;
    public uint MaxStack => maxStack;
    public int Price => price;

    public int Damage => damage;
    public int Defence => defence;
    public float Speed => speed;
}
