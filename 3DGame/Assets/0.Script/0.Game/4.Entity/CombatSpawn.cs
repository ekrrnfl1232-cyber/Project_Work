using UnityEngine;

public class CombatSpawn : MonoBehaviour
{
    public BoxCollider spawnArea;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameEvents.RaiseChangeEnPlayer(spawnArea);
        }
    }
}
