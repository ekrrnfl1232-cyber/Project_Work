using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSkill
{
    Area
}

public class PlayerView : MonoBehaviour
{

    [Header("Skill")]
    [SerializeField] public GameObject area;

    [Header("UI")]
    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHP;
    [SerializeField] private GameObject UiCheckBox;

    [SerializeField] private Image expImg;
    [SerializeField] public TMP_Text exptext;
    [Header("Animator")]
    [SerializeField] private Animator animator;
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
        float ratio = maxHp > 0 ? Mathf.Clamp01((float)hp / maxHp) : 0f;
        hpImg.rectTransform.sizeDelta = new Vector2(200f * ratio, 30f);
    }

    public void ExpUpdata()
    {
        exptext.text = $"LV.{PlayerStat.Level} {PlayerStat.Exp / PlayerStat.MaxExp * 100f}% ({PlayerStat.Exp} / {PlayerStat.MaxExp})";
        expImg.rectTransform.sizeDelta = new Vector2(1920f * (PlayerStat.Exp / PlayerStat.MaxExp), 20f);
    }

    public void Area(Vector3 pos)
    {
        area.SetActive(true);
        area.transform.position = pos;
    }

    private void OnEnable()
    {
        GameEvents.ChangeEXPUpdate += ExpUpdata;
    }

    private void OnDisable()
    {
        GameEvents.ChangeEXPUpdate -= ExpUpdata;
    }
}
