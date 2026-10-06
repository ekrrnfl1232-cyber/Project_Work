using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListPopup : MonoBehaviour
{
    [SerializeField] private List<Image> listImage = new List<Image>();
    [SerializeField] private TMP_Text txt;

    private int showIndex = 0;

    private void Update()
    {
    }

    public void On(string str)
    {
        int index = showIndex;

        txt.text = str;
        listImage[index].color = new Color(0, 0, 0, 180f/225f);
        listImage[index].gameObject.SetActive(true);

        listImage[index].DOFade(0f, 0.5f)
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
                listImage[index].rectTransform.localPosition = new Vector3(0f, -225f, 0f);
                listImage[index].gameObject.SetActive(false);
            });
        Sequence sequence = DOTween.Sequence();
        sequence.Append
            (
                listImage[index].rectTransform
                    .DOAnchorPos(new Vector2(0f, -25f), 1f)
            );
        showIndex++;
        if( showIndex >= listImage.Count )
        {
            showIndex = 0;
        }
    }
}
