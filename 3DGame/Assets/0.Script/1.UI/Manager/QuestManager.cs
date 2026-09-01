using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private readonly List<QuestProgress> activeQuests = new();
    public IReadOnlyList<QuestProgress> ActiveQuest => activeQuests;
    public Action OnQuestChanged;
    
    public bool AcceptQuest(QuestData quest)
    {
        if (quest == null)
            return false;
        if(HasQuest(quest.questId))
        {
            Debug.Log("이미 가지고 있는 퀘스트");
            return false;
        }
        QuestProgress prog = new QuestProgress(quest);
        activeQuests.Add(prog);
        OnQuestChanged?.Invoke();
        Debug.Log($"Quest Accepted:{quest.questTitle}");
        return true;
    }

    public bool HasQuest(int questId)
    {
        foreach (QuestProgress quest in activeQuests)
        {
            if(quest.Data.questId == questId)
            {
                return true;
            }
        }
        return false;
    }

    public QuestProgress GetQuest(int questId)
    {
        foreach(QuestProgress quest in activeQuests)
        {
            if(quest.Data.questId == questId)
            {
                return quest;
            }
        }
        return null;
    }

    public void NotifyEnemyKilled(int enemyId)
    {
        bool change = false;

        foreach(QuestProgress quest in activeQuests)
        {
            if (quest.State != QuestState.Inprogress)
                continue;
            if (quest.Data.questType != QuestType.Kill)
                continue;
            if (quest.Data.targetId != enemyId)
                continue;

            quest.Addprogress(1);
            change = true;
            Debug.Log($"{quest.Data.questTitle} : {quest.CurrentCount}/{quest.Data.requiredCount}");
        }

        if(change == true)
        {
            OnQuestChanged?.Invoke();
        }
    }

    public bool CompleteQuest(int questId)
    {
        QuestProgress quest = GetQuest(questId);
        if (quest == null)
            return false;
        if(quest.State != QuestState.canComplete)
            return false;
        QuestData data = quest.Data;
        if(data.rewardItem != null && data.rewardItemCount != 0)
        {
            int remaining = UIConstroller.Instance.inventory.CreateItem(data.rewardItem, data.rewardItemCount);
            if(remaining > 0)
            {
                Debug.Log("가방 공간 부족");
                return false;
            }
        }
        int gold = quest.Data.rewardGold;
        PlayerProgress.Instance.AddExp(gold);
        float exp = (float)quest.Data.rewardExp;
        PlayerProgress.Instance.AddExp(exp);
        
        quest.Complete();
        OnQuestChanged?.Invoke();
        Debug.Log($"Quest Complete {quest.Data.questTitle}");
        return false;
    }
}
