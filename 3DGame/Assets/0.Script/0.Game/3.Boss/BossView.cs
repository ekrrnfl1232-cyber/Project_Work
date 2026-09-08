using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour
{

    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHp;
    [SerializeField] private GameObject areaPrefab;
    [SerializeField] private GameObject chargePrefab;

    private GameObject hpBG;
    private Image hpImgOne;
    private Image hpImgTwo;

    public void CreateHp()
    {
        hpBG = Instantiate(prefabHp, isParent);
        hpImgOne = hpBG.transform.GetChild(1).GetComponent<Image>();
        hpImgTwo = hpBG.transform.GetChild(0).GetComponent<Image>();
        hpBG.transform.position = new Vector3(25, 500, 0);
    }

    public void UpdateHp(int hp, int maxHp, int phase)
    {
        if (phase == 1)
            hpImgOne.rectTransform.sizeDelta = new Vector2(hpImgOne.rectTransform.sizeDelta.x * ((float)hp / maxHp), 20f);
        else if (phase == 2)
            hpImgTwo.rectTransform.sizeDelta = new Vector2(hpImgTwo.rectTransform.sizeDelta.x * ((float)hp / maxHp), 20f);
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
