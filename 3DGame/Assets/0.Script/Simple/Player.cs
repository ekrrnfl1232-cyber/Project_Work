using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Move")]
    [SerializeField] public float moveForce = 4f;

    [Header("Jump")]
    [SerializeField] public float jumpForce = 40f;

    [Header("Dash")]
    [SerializeField] public GameObject dashShadow;
    [SerializeField] public float dashForce = 200f;
    [SerializeField] public float dashDuration = 0.2f;

    [Header("InteractScale")]
    [SerializeField] private float InterationScale = 2f;

    [Header("WeaponDamage")]
    [SerializeField] public int wDamage = 10;

    [Header("UI")]
    [SerializeField] public GameObject UiCheckBox;
    [SerializeField] public GameObject hpBG;
    [SerializeField] public Image hpImg;

    [Header("HP")]
    public int hp;
    public int maxhp;

    [Header("Animator")]
    [SerializeField] public Animator animator;

    private bool isGrounded = true;

    public Vector3 movement = new Vector3();
    private LayerMask groundLayer;


    private IState currentState;
    public Rigidbody rb;

    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground", "Box");
    }

    private void Start()
    {
        hp = maxhp = 100;
        ChangeState(new PlayerIdle(this));
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            ChangeState(new PlayerAttackState(this, currentState));
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ChangeState(new PlayerJumpState(this, currentState, rb));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeState(new PlayerDashState(this, currentState, rb));
        }
        Interect();
        HpBar();
        Look();
        currentState?.Tick();
    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
    public void Dash(IState prevState)
    {
        animator.SetBool("ShieldRush", true);
        rb.AddRelativeForce(Vector3.forward * dashForce, ForceMode.VelocityChange);
        Invoke("StopDash", 0.2f);
        ChangeState(prevState);
    }
    void StopDash()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        animator.SetBool("ShieldRush", false);
    }

    public void TakeDamage(int damage)
    {
        ChangeState(new PlayerHitState(this, currentState, damage));
    }

    void Interect()
    {
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
    }
    void HpBar()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y += 50f;
        hpBG.transform.position = pos;
    }
    private void Look()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                //transform.forward = dir;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 tpos = transform.position;
        Gizmos.DrawWireCube(tpos, new Vector3(0.5f, 0.2f, 0.5f));
    }
}
