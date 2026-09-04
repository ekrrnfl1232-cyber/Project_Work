using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour
{
    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHP;

    private GameObject hpBG;
    private Image hpImg;

    public void CreateHp()
    {
        hpBG = Instantiate(prefabHP, isParent);
        hpImg = hpBG.transform.GetChild(0).GetComponent<Image>();
    }
    public void DeleteHp()
    {
        Destroy(hpBG);
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

    public void HPbarDelete(bool isLive)
    {
        hpBG.SetActive(isLive);
    }
}
