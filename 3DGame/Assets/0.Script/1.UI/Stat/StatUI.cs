using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] private StatUIView view;
    [SerializeField] private GameObject UiStat;
    [SerializeField] private List<PlusStatSlot> StatSlots;
    public int LevelStat { get; set; } = 5;

    private void Awake()
    {
        GameEvents.LevelChange += LevelChange;
        view.TotalStat();
        view.LevelView(LevelStat);
    }

    private void Update()
    {
            

    }

    public void LevelChange()
    {
        LevelStat += 1;
        view.LevelView(LevelStat);
    }

    public void UsedLevelStat()
    {
        LevelStat -= 1;
        view.LevelView(LevelStat);
    }
}
