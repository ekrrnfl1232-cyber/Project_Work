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
}
