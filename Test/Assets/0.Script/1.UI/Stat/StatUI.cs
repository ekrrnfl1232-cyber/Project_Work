using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : Singleton<StatUI>
{
    [SerializeField] public StatUIView view;
    [SerializeField] private GameObject UiStat;
    [SerializeField] private List<PlusStatSlot> StatSlots;

    public int TotalStr { get; private set; }
    public int TotalHp { get; private set; }
    public int TotalDef { get; private set; }
    public int LevelStat { get; set; } = 5;

    private void Awake()
    {

       
        view.LevelView();
    }

    private void Update()
    {
        TotalStr = StatSlots[0].TotalStat;
        TotalHp = StatSlots[1].TotalStat;
        TotalDef = StatSlots[2].TotalStat;
        view.TotalStatUpdate();
    }
    public void LevelChange()
    {
        LevelStat += 1;
        view.LevelView();
    }
}
