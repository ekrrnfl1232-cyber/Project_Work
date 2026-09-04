using UnityEngine;

public enum QuestState
{
    Inprogress, canComplete, Completed
}

public enum QuestType
{
    Kill, Collect
}

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    [Header("Info")]
    public int questId;
    public string questTitle;

    [TextArea]
    public string description;

    [Header("Condition")]
    public QuestType questType;
    public int targetId;
    public int requiredCount = 1;

    [Header("Reward")]
    public int rewardGold;
    public int rewardExp;
    public ItemScriptable rewardItem;
    public int rewardItemCount;
}
