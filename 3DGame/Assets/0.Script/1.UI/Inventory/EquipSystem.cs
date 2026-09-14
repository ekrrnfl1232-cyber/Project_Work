using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class EquipSystem : Singleton<EquipSystem>
{
    public EquipmentSlot[] slots;

    public EquipmentSlot SelectSlot { get; set; }
    
    public int EquipTotalDamage()
    {
        int totalDamage = 0;
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalDamage += slot.Data.Damage;
            }
        }
        return totalDamage;
    }

    public int EquipTotalDefence()
    {
        int totalDefence = 0;
        foreach(EquipmentSlot slot in slots)
        {
            if(slot.Data != null && slot.Data.Defence != 0)
            {
                totalDefence += slot.Data.Defence;
            }
        }
        return totalDefence;
    }

    public float EquipTotalSpeed()
    {
        float totalSpeed = 0;

        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null && slot.Data.Speed != 0)
            {
                totalSpeed += slot.Data.Speed;
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