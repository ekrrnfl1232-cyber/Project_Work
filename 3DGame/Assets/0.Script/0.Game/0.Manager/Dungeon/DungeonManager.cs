using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject door;

    public int targetCount;
    private int killCount;
    void Start()
    {
        killCount = 0;
        door.SetActive(false);
        GameEvents.PlayerKill += (data, gold, exp) => Target();
    }

    private void Update()
    {
        if(killCount == targetCount)
        {
            door.SetActive(true);
        }
    }

    private void Target()
    {
        killCount++;
    }
}
