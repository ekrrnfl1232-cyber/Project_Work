using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour
{

    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHp;

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
}
