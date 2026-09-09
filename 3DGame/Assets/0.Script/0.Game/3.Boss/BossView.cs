using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour
{

    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHp;
    [SerializeField] private GameObject areaPrefab;
    [SerializeField] private GameObject chargePrefab;
    [SerializeField] private TMP_Text phaseTxt;

    private GameObject hpBG;
    private Image hpImgOne;
    private Image hpImgTwo;

    public void CreateHp(BossStat stats)
    {
        hpBG = Instantiate(prefabHp, isParent);
        hpImgOne = hpBG.transform.GetChild(1).GetComponent<Image>();
        hpImgTwo = hpBG.transform.GetChild(0).GetComponent<Image>();
        RectTransform rt = hpBG.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0f, -50f);
    }

    public void UpdateHp(BossStat stats)
    {
        hpImgOne.rectTransform.sizeDelta = new Vector2(hpImgOne.rectTransform.sizeDelta.x * ((float)stats.HpOne / (stats.MaxHp / 2)), 20f);
        hpImgTwo.rectTransform.sizeDelta = new Vector2(hpImgTwo.rectTransform.sizeDelta.x * ((float)stats.HpTwo / (stats.MaxHp / 2)), 20f);
        if(stats.HpOne == 0)
        {
            phaseTxt.text = "¡¿ 1";
        }
    }

    public GameObject AreaAttack(Vector3 targetPos)
    {
        GameObject areaAttack = Instantiate(areaPrefab, targetPos, areaPrefab.transform.rotation);

        return areaAttack;
    }
    
    public GameObject ChargeAttack()
    {
        Vector3 spawnArea = transform.position + transform.forward * 1.5f;
        Quaternion rotaitionArea = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);
        GameObject chargeAttack = Instantiate(chargePrefab, spawnArea, rotaitionArea);

        return chargeAttack;
    }

    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }

}
