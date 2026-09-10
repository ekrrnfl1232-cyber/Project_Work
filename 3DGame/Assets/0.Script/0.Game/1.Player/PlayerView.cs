using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHP;
    [SerializeField] private GameObject UiCheckBox;

    [SerializeField] private Image expImg;
    [SerializeField] public TMP_Text exptext;

    private GameObject hpBG;
    private Image hpImg;
    [SerializeField]private PlayerStat stats;

    public void CheckBox(bool isFind)
    {
        UiCheckBox.SetActive(isFind);
    }

    public void CreateHp()
    {
        hpBG = Instantiate(prefabHP, isParent);
        hpImg = hpBG.transform.GetChild(0).GetComponent<Image>();
    }


    public void HPbar(Vector3 pos)
    {
        Vector3 targetPos = Camera.main.WorldToScreenPoint(pos);
        targetPos.y += 150f;
        hpBG.transform.position = targetPos;
    }

    public void HpUpdate(int hp, int maxHp)
    {
        hpImg.rectTransform.sizeDelta = new Vector2(hpImg.rectTransform.sizeDelta.x * ((float)hp / maxHp), 30f);
    }

    public void ExpUpdata(float exp)
    {
        stats.Exp += exp;
        if (stats.Exp >= stats.MaxExp)
        {
            while(stats.Exp >= stats.MaxExp)
            {
                stats.Exp -= stats.MaxExp;
                stats.Level += 1;
                stats.MaxExp += 100f;
            }
        }
        exptext.text = $"LV.{stats.Level} {stats.Exp / stats.MaxExp * 100f}% ({stats.Exp} / {stats.MaxExp})";
        expImg.rectTransform.sizeDelta = new Vector2(1920f * (stats.Exp / stats.MaxExp), 20f);
    }

    private void OnEnable()
    {
        GameEvents.OnExpChange += ExpUpdata;
    }

    private void OnDisable()
    {
        GameEvents.OnExpChange -= ExpUpdata;
    }
}
