using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour
{

    private GameObject hpBG;
    private Image hpImg;

    public void CreateHp(Vector3 pos)
    {
        hpBG = ObjectPoolManager.Instance.GetObject(PoolType.HpBar);
        hpImg = hpBG.transform.GetChild(0).GetComponent<Image>();
        HPbar(pos);
        hpBG.SetActive(true);
    }
    public void ReturnHp()
    {
        ObjectPoolManager.Instance.ReturnObject(PoolType.HpBar, hpBG);
    }

    public void HPbar(Vector3 pos)
    {
        Vector3 targetPos = Camera.main.WorldToScreenPoint(pos);
        targetPos.y += 100f;
        hpBG.transform.position = targetPos;
    }

    public void HpUpdate(int HP, int maxHP)
    {
        hpImg.rectTransform.sizeDelta = new Vector2(hpImg.rectTransform.sizeDelta.x * ((float)HP / maxHP), hpImg.rectTransform.sizeDelta.y);
    }
}
