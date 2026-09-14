using TMPro;
using UnityEngine;

public class PlusStatSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text plusTxt;
    [SerializeField] private TMP_Text totalTxt;
    [SerializeField] private StatUI statUI;
    public int TotalStat { get; private set; }
    private int plusStat = 0;

    public void Plus()
    {
        if (statUI.LevelStat != 0)
        {
            plusStat++;
            plusTxt.text = $"{plusStat}";
        }
    }

    public void Minus()
    {
        if (plusStat != 0)
        {
            plusStat--;
            plusTxt.text = $"{plusStat}";
        }
    }

    public void AcceptStat()
    {
        TotalStat += plusStat;
        plusStat = 0;
        totalTxt.text = $"{TotalStat}";
        plusTxt.text = $"{plusStat}";
    }

    public void ResetStat()
    {
        plusStat = 0;
        plusTxt.text = $"{plusStat}";
    }
}
