using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUIView : MonoBehaviour
{

    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private List<TMP_Text> stat;
    [SerializeField] private TMP_Text levelStat;
    [SerializeField] private StatUI statUI;

    public void LevelView()
    {
        levelStat.text = $"{statUI.LevelStat}";
    }

    public void TotalStatUpdate ()
    {
        stat[0].text = $"{playerStat.TotalHP()}";
        stat[1].text = $"{playerStat.TotalDamage()}";
        stat[2].text = $"{playerStat.TotalDefence()}";
        stat[3].text = $"{playerStat.TotalSpeed()}";
    }
}
