using UnityEngine;

public class QuestProgress
{
    public QuestData Data {  get; private set; }

    public int CurrentCount { get; private set; }

    public QuestState State { get; private set; }

    public QuestProgress(QuestData data)
    {
        Data = data;
        CurrentCount = 0;
        State = QuestState.Inprogress;
    }

    public void Addprogress (int amount = 1)
    {
        if (State != QuestState.Inprogress)
            return;
        CurrentCount += amount;
        if(CurrentCount >= Data.requiredCount)
        {
            CurrentCount = Data.requiredCount;
            State = QuestState.canComplete;
        }
    }

    public void Complete()
    {
        if(State != QuestState.canComplete)
        {
            return;
        }
        State = QuestState.Completed;
    }
}
