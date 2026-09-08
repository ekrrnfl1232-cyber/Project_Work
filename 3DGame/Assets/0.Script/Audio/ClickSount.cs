using UnityEngine;
using UnityEngine.UI;

public class ClickSount : MonoBehaviour
{
    [SerializeField] private ClipType clipType;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.EffectSound(clipType));
    }

}
