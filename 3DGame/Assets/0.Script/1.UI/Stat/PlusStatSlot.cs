using TMPro;
using UnityEngine;

public class PlusStatSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text plusTxt;
    [SerializeField] private TMP_Text totalTxt;
    [SerializeField] private StatUI statUI;
    public int TotalStat { get; private set; } = 0;
    private int plusStat = 0;

    public void Plus()
    {
        if (statUI.LevelStat != 0)
        {
            plusStat++;
            statUI.LevelStat--;
            plusTxt.text = $"{plusStat}";
            statUI.view.LevelView();
        }
    }

    public void Minus()
    {
        if (plusStat != 0)
        {
            plusStat--;
            statUI.LevelStat++;
            plusTxt.text = $"{plusStat}";
            statUI.view.LevelView();
        }
    }

    public void AcceptStat()
    {
        TotalStat += plusStat;
        plusStat = 0;
        totalTxt.text = $"{TotalStat}";
        plusTxt.text = $"{plusStat}";
        statUI.view.TotalStatUpdate();
    }

    public void ResetStat()
    {
        statUI.LevelStat += plusStat;
        plusStat = 0;
        plusTxt.text = $"{plusStat}";
        statUI.view.LevelView();
    }
}
