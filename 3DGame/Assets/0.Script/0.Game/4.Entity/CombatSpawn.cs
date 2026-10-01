using UnityEngine;

public class CombatSpawn : MonoBehaviour
{
    public BoxCollider spawnArea;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("콜리더 감지");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 감지");
            GameEvents.RaiseChangeEnPlayer(spawnArea);
            transform.gameObject.SetActive(false);
        }
    }
}
