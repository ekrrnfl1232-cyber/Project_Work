using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Monster : MonoBehaviour, IDamageable
{

    [SerializeField] private GameObject hpBG;
    [SerializeField] private Image hpImg;

    [SerializeField] private float atkDuration = 2f;
    [SerializeField] private float atkTimer = 0f;

    [SerializeField] private int mDamage = 5;

    [SerializeField] private Transform target;

    [SerializeField] public int hp, maxhp = 100;

    void Start()
    {
        hp = maxhp = 100;
        atkTimer = atkDuration;
    }
    void Update()
    {
        HpBar();
        if (target == null)
            return;

        LookAtMove();
    }

    private void LookAtMove()
    {
        Vector3 tPos = target.position;
        Vector3 mPos = transform.position;
        Vector3 pPos = target.position;
        tPos.y = transform.position.y;
        transform.LookAt(tPos);
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance > 1.5f)
        {
            mPos.y = 0;
            pPos.y = 0;
            transform.position = Vector3.MoveTowards(mPos, pPos, 1f * Time.deltaTime);
        }
        else
        {
            atkTimer -= Time.deltaTime;
            if (atkTimer <= 0)
            {
                Debug.Log($"{name}공격");
                Attack();
                atkTimer = atkDuration;
            }
        }
    }

    private void HpBar()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y += 50f;
        hpBG.transform.position = pos;
    }

    public void Attack()
    {
        Vector3 posAttack = transform.position + transform.forward * 1f;
        posAttack.y += 0.7f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.2f, 1.4f, 0.8f), transform.rotation, LayerMask.GetMask("Player"));

        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(mDamage);
                break;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpImg.rectTransform.sizeDelta = new Vector2(50f * ((float)hp / maxhp), 10f);
        if (hp <= 0)
        {
            Debug.Log($"{name} Dead");
            gameObject.SetActive(false);
            hpBG.SetActive(false);
        }
        else
            Debug.Log($"남은 체력 : {hp}" );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 posAttack = transform.position + transform.forward * 1f;
        posAttack.y += 0.7f;
        Gizmos.DrawWireCube(posAttack, new Vector3(1f, 1.4f, 0.7f));
    }
}
