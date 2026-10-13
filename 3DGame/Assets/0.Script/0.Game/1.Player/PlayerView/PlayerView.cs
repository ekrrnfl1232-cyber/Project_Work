using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSkill
{
    Area
}

public class PlayerView : MonoBehaviour
{

    [System.Serializable]
    private class SkillCooldownSlot
    {
        public PlayerCool skill;
        public Image fillImage;
    }

    [Header("Create in Parent")]
    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHP;

    [Header("Interaction")]
    [SerializeField] private GameObject UiCheckBox;

    [Header("EXPbar")]
    [SerializeField] private Image expImg;
    [SerializeField] public TMP_Text exptext;

    [Header("HPbar")]
    private GameObject hpBG;
    private Image hpImg;

    [Header("PlayerScript")]
    [SerializeField] private Player player;
    [Header("Skill UI")]
    [SerializeField] private SkillCooldownSlot[] cooldownSlots;

    private void Update()
    {
        HPbar(transform.position);
        SkillCooldown();
    }
    public void CheckBox(bool isFind)
    {
        UiCheckBox.SetActive(isFind);
    }

    public void CreateHp()
    {
        hpBG = Instantiate(prefabHP, isParent);
        hpImg = hpBG.transform.GetChild(0).GetComponent<Image>();
    }

    private void SkillCooldown()
    {
        foreach (var slot in cooldownSlots)
        {
            if(slot == null || slot.fillImage == null)
                continue;

            slot.fillImage.fillAmount = player.Cool.GetTime(slot.skill);
        }
    }
    private void HPbar(Vector3 pos)
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

    private void OnEnable()
    {
        GameEvents.ChangeEXPUpdate += ExpUpdata;
    }

    private void OnDisable()
    {
        GameEvents.ChangeEXPUpdate -= ExpUpdata;
    }
}
