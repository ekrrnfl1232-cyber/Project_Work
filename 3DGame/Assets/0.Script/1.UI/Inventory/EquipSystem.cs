using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class EquipSystem : Singleton<EquipSystem>
{
    public EquipmentSlot[] slots;

    public EquipmentSlot SelectSlot { get; set; }
    int totalDamage = 0;
    float totalSpeed = 0;

    public int EquipTotalDamage()
    {
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalDamage += slot.Data.Damage;
            }
        }
        return totalDamage;
    }
    public float EquipTotalSpeed()
    {
        

        foreach(EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalSpeed += slot.Data.Speed;
            }
            else
            {
                totalSpeed = 0;
            }
        }

        return totalSpeed;
    }

    public EquipData[] GetEquipData()
    {
        EquipData[] data = new EquipData[slots.Length];

        for(int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Data == null)
            {
                data[i] = new EquipData
                {
                    id = 1234
                };
                continue;
            }
            data[i] = new EquipData
            {
                id = slots[i].Data.ItemID
            };
        }

        return data;
    }
    public void LoadEquip(EquipData[] data)
    {
        foreach(var equipData in data)
        {
        }
    }
}