using UnityEngine;


public enum VFXtype
{
    Heal,
    AreaAttack,
    ChargeAttack
}

public class VFXManager : Singleton<VFXManager>
{
    [System.Serializable]
    public class VFXData
    {
        public VFXtype type;
        public GameObject vfxObj;
    }
    [SerializeField] private VFXData[] vfxDatas;

    public void Show(VFXtype fxType, Transform tran)
    {
        Quaternion rotaitionArea = Quaternion.Euler(0f, tran.eulerAngles.y, 0f);
        foreach (var vfx in vfxDatas)
        {
            if(fxType == vfx.type)
            {
                Instantiate(vfx.vfxObj, tran.position, rotaitionArea).transform.SetParent(transform);

                break;
            }
        }
    }
}
