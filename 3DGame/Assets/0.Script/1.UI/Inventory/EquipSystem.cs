using System.Linq;
using UnityEngine;

public class EquipSystem : Singleton<EquipSystem>
{
    public EquipmentSlot[] slots;

    public EquipmentSlot SelectSlot { get; set; }

    public int EquipTotalDamage()
    {
        int totalValue = 0;

        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalValue += slot.Data.Damage;
            }
        }

        return totalValue;
    }
    public float EquipTotalSpeed()
    {
        float totalValue = 0;

        foreach(EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalValue += slot.Data.Speed;
            }
        }

        return totalValue;
    }
}
