using TMPro;
using UnityEngine;

public class QuestUi : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text progressTxt;

    private void Start()
    {
        GameEvents.OnQuestChanged += ReFresh;
    }
    private void OnDisable()
    {
        if (questManager != null)
            GameEvents.OnQuestChanged -= ReFresh;
    }

    private void ReFresh()
    {
        if (questManager.ActiveQuest.Count == 0)
        {
            titleTxt.text = string.Empty;
            progressTxt.text = string.Empty;
            gameObject.SetActive(false);
            return;
        }
        QuestProgress quest = questManager.ActiveQuest[0];
        titleTxt.text = quest.Data.questTitle;
        gameObject.SetActive(true);
        switch (quest.State)
        {
            case QuestState.Inprogress:
                progressTxt.text = $"{quest.CurrentCount}/{quest.Data.requiredCount}";
                break;
            case QuestState.Completed:
                progressTxt.text = "퀘스트 완료";
                break;
            case QuestState.canComplete:
                progressTxt.text = "퀘스트 완료 가능";
                break;

        }
    }
}
