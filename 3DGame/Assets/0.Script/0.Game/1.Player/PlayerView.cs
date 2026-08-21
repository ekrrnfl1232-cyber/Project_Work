using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PlayerView : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHP;
    [SerializeField] private GameObject UiCheckBox;

    private PlayerModel model;
    private GameObject hpBG;
    private Image hpImg;

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
        targetPos.y += 50f;
        hpBG.transform.position = targetPos;
    }

    public void HpUpdate(int hp, int maxHp)
    {
        hpImg.rectTransform.sizeDelta = new Vector2(50f * ((float)hp / maxHp), 10f);
    }

}
