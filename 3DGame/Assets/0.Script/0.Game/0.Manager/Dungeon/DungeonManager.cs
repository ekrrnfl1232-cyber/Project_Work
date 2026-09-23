using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject door;

    [SerializeField] private TMP_Text count;

    private int targetCount;

    private int killCount;
    void Start()
    {
        killCount = 0;
        targetCount = 10;
        door.SetActive(false);
        GameEvents.PlayerKill += (data, gold, exp) => Target();
    }

    private void Update()
    {
        if(killCount >= targetCount)
        {
            door.SetActive(true);
        }
    }

    private void Target()
    {
        killCount++;
        count.text = $"{ killCount }/{ targetCount }";
    }

    private void OnDestroy()
    {
        GameEvents.PlayerKill -= (data, gold, exp) => Target();
    }
}
