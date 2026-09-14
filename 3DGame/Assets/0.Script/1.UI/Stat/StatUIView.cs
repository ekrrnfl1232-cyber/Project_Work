using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUIView : MonoBehaviour
{

    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private List<TMP_Text> stat;
    [SerializeField] private TMP_Text levelStat;

    public void LevelView(int value)
    {
        levelStat.text = $"{value}";
    }

    public void TotalStat ()
    {
        stat[0].text = $"{playerStat.MaxHp}";
        stat[1].text = $"{playerStat.TotalDamage()}";
        stat[2].text = $"{playerStat.TotalDefence()}";
        stat[3].text = $"{playerStat.TotalSpeed()}";
    }
}
