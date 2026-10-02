using System.Collections.Generic;
using UnityEngine;

public class CombatSensor : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private Transform combat;
    [Header("Block")]
    [SerializeField] private GameObject blockWall;

    private bool isUse = false;

    private void OnEnable()
    {
        GameEvents.ClearCombat += IsClear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !isUse)
        {
            isUse = true;
            GameEvents.RaiseChangeEnPlayer(combat);
            blockWall.SetActive(true);
        }
    }

    private void IsClear()
    {
        if (transform.gameObject.activeInHierarchy)
        {
            blockWall.SetActive(false);
            transform.gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        GameEvents.ClearCombat -= IsClear;
    }
}
