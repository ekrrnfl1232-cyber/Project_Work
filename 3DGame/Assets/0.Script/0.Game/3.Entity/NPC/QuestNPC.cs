using UnityEngine;

public class QuestNPC : MonoBehaviour, IInterectable
{
    [SerializeField] private QuestData questData;
    [SerializeField] private QuestManager questManager;

    public string GetInterfaction()
    {
        QuestProgress quest = questManager.GetQuest(questData.questId);
        if (quest == null)
            return "[F] 퀘스트받기";
        if (quest.State == QuestState.canComplete)
            return "[F] 퀘스트 완료";
        if (quest.State == QuestState.Inprogress)
            return $"[F]진행도:{quest.CurrentCount}/{questData.requiredCount}";

        return "[F] 대화하기";
    }
    public void Interact()
    {
        QuestProgress quest = questManager.GetQuest(questData.questId);
        if (quest == null)
        {
            questManager.AcceptQuest(questData);
            return;
        }
        if(quest.State == QuestState.canComplete)
        {
            questManager.CompleteQuest(questData.questId);
            return;
        }
        Debug.Log("용무 없음");
    }

}
