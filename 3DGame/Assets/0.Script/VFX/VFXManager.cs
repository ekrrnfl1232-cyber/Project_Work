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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Vector3 pos = FindAnyObjectByType<Player>().transform.position;
            Show(VFXtype.Heal, pos);
        }
    }

    public void Show(VFXtype fxType, Vector3 pos)
    {
        foreach(var vfx in vfxDatas)
        {
            if(fxType == vfx.type)
            {
                Instantiate(vfx.vfxObj, pos, Quaternion.identity).transform.SetParent(transform);

                break;
            }
        }
    }
}
