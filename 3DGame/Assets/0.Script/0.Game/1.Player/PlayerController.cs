using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Vector3 movement = new Vector3();
    [SerializeField] private float moveForce = 4f;

    [SerializeField] private float jumpForce = 4000f;

    [SerializeField] private float dashForce = 20f;

    [SerializeField] private float raylength = 1.005f;

    [SerializeField] private float InterationScale = 2f;

    [SerializeField] private int wDamage = 10;

    public GameObject UiCheckBox;

    [SerializeField] private GameObject hpBG;
    [SerializeField] private Image hpImg;

    public int hp, maxhp;

    private bool isGrounded = true;

    LayerMask groundLayer;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground", "Box");
    }

    private void Start()
    {
        hp = maxhp = 100;
    }
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            Move();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        Debug.DrawRay(transform.position, Vector3.down * raylength, Color.red);
        Vector3 center = transform.position;
        isGrounded = Physics.CheckBox(center, new Vector3(0.5f, 0.1f, 0.5f), transform.rotation, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        Look();
        HpBar();
        Vector3 posInter = transform.position;
        posInter.y += 1f;
        Collider[] colls = Physics.OverlapSphere(posInter, InterationScale);
        bool isFind = false;

        foreach (var col in colls)
        {
            if (col.TryGetComponent<IInterectable>(out IInterectable interact))
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

    private void HpBar()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y += 50f;
        hpBG.transform.position = pos;
    }

    private void Attack()
    {
        Vector3 posAttack = transform.position + transform.forward * 1f;
        posAttack.y += 0.5f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.4f, 1.4f, 1f), transform.rotation, LayerMask.GetMask("Monster"));

        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(wDamage);
                break;
            }
        }
    }

    public void TakeDamage(int damage) 
    {
        hp -= damage;
        Debug.Log($"{name} 타격 받음");
        Debug.Log($"남은 체력 : {hp}");
        hpImg.rectTransform.sizeDelta = new Vector2(50f * ((float)hp / maxhp), 10f);
        if (hp <= 0)
        {
            Debug.Log($"{name} Dead");
        }
        
    }
    public void Look()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;

            transform.forward = dir;
        }

    }

    public void OnDash(InputValue shift)
    {
        rb.AddRelativeForce(Vector3.forward * dashForce, ForceMode.VelocityChange);
        Invoke("StopDash", 0.2f);
    }

    
    void StopDash()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
    }
    public void Move()
    {
        transform.Translate(movement * Time.deltaTime * moveForce, Space.World);
    }

    public void Jump()
    {
        if(isGrounded)
            rb.linearVelocity = new Vector3(0f, Mathf.Sqrt(5f * 9.81f * jumpForce), 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 posit = transform.position + transform.forward * 1f;
        posit.y += 0.5f;
        Gizmos.DrawWireCube(posit, new Vector3(1.5f, 1.5f, 1.4f));
    }
}
