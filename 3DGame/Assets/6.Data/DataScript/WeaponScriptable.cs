using UnityEngine;

[CreateAssetMenu]
public class WeaponScriptable : ScriptableObject
{
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private float range;
    public float WeaponRange { get { return range; } }
}
