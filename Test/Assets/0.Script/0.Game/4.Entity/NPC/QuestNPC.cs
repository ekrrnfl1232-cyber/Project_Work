using TMPro;
using UnityEngine;

public class QuestNPC : MonoBehaviour, IInterectable
{
    [SerializeField] private QuestData questData;
    [SerializeField] private QuestManager questManager;

    [SerializeField] private GameObject questUI;
    [SerializeField] private TMP_Text questTitle;
    [SerializeField] private TMP_Text questInfo;


    private void Awake()
    {
        questUI.SetActive(false);

    }

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
            questTitle.text = $"{questData.questTitle}";
            questInfo.text = $"{questData.description} \n 보상 : {questData.rewardItem.name} 경험치 : {questData.rewardExp}";
            questUI.SetActive(true);
            return;
        }
        if(quest.State == QuestState.canComplete)
        {
            questManager.CompleteQuest(questData.questId);
            return;
        }
        Debug.Log("용무 없음");
    }

    public void Accept()
    {
        questManager.AcceptQuest(questData);
        questUI.SetActive(!questUI.activeSelf);
    }

}
