using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

public class ToastPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private HorizontalLayoutGroup hlg;

    private Queue<string> strings = new Queue<string>();
    private bool isAnimation = false;

    public void OnPopup(string str)
    {
        strings.Enqueue(str);
    }

    private void Update()
    {
        if(strings.Count != 0 && !isAnimation)
        {
            txt.text = strings.Dequeue();
            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        isAnimation = true;

        hlg.enabled = false;
        yield return new WaitForSeconds(0.2f);
        hlg.enabled = true;
        GetComponent<Image>().rectTransform
            .DOAnchorPos(new Vector2(0f, 180f), 0.2f)
            .SetEase(Ease.OutBack)
            .SetUpdate(false)
            .OnComplete(() =>
            {
                GetComponent<Image>().rectTransform
                    .DOAnchorPos(new Vector2(0f, -80f), 0.1f)
                    .SetDelay(2f)
                    .SetEase(Ease.Linear)
                    .SetUpdate(false)
                    .OnComplete(() => isAnimation = false);
            });
        yield return null;
    }
}
