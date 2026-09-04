using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageFontManager : Singleton<DamageFontManager>
{
    [SerializeField] TMP_Text damageTxt;

    public float jumpHeight = 2f;
    public float duration = 0.8f;

    public void CreateText(int damage, Vector3 pos)
    {
        Vector3 uiPos = Camera.main.WorldToScreenPoint(pos);
        TMP_Text txt = Instantiate(damageTxt, uiPos, Quaternion.identity, transform);
        txt.text = $"{damage}";
        Vector3 startPos = uiPos;
        Sequence seq = DOTween.Sequence();

        seq.Append(txt.rectTransform.DOMoveY(startPos.y + jumpHeight, duration * 0.45f)
            .SetEase(Ease.OutQuad));
        seq.Append(txt.rectTransform.DOMoveY(startPos.y, duration * 0.55f)
            .SetEase(Ease.InQuad));
        seq.Append(txt.rectTransform.DOScale(Vector3.zero, 0.15f)
            .SetEase(Ease.InBack));
        seq.OnComplete(() =>
        {
            Destroy(txt.gameObject);
        });
    }
}
