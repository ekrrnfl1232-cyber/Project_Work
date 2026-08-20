using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteration : MonoBehaviour
{
    private float InterationScale = 1.5f;

    [SerializeField] private int WDamage = 10;

    public GameObject UiCheckBox;
    void Update()
    {
        Vector3 posInter = transform.position;
        posInter.y += 1f;
        Collider[] colls = Physics.OverlapSphere(posInter, InterationScale);
        bool isFind = false;
        
        foreach(var col in colls)
        {
            if(col.TryGetComponent<IInterectable>(out IInterectable interact))
            {
                isFind = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interact.Interact();
                }
                break;
            }
        }
        UiCheckBox.SetActive(isFind);

        if (Input.GetMouseButtonDown(0))
            Attack();

    }

    private void Attack()
    {
        Vector3 posAttack = transform.position + transform.forward * 1f;
        posAttack.y += 0.5f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.4f, 1.4f, 1f), transform.rotation);

        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(WDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 posit = transform.position + transform.forward * 1f;
        posit.y += 0.5f;
        Gizmos.DrawWireCube(posit, new Vector3(1.5f, 1.5f, 1.4f));
    }
}
