using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{

    [Header("Move")]
    [SerializeField] public float moveForce = 4f;
    public Vector3 movement = Vector3.zero;

    [Header("Jump")]
    [SerializeField] public float jumpForce = 40f;

    [Header("Dash")]
    [SerializeField] public GameObject dashShadow;
    [SerializeField] public float dashForce = 200f;

    [Header("InteractScale")]
    [SerializeField] private float InterationScale = 2f;

    [Header("WeaponDamage")]
    [SerializeField] public int wDamage = 10;

    [Header("HP")]
    [SerializeField] private int HP = 100;

    [Header("Animator")]
    [SerializeField] public Animator animator;
    private bool comboWindowOpen;
    private bool comboWindowWasOpened;
    private bool comboBroken;

    private bool isGrounded;
    private IState currentState;
    public Rigidbody rb;

    public Cooldown coolDown = new Cooldown(1f);
    public PlayerView view;
    public PlayerModel model;

    private void Awake()
    {
        model = new PlayerModel
            (
            moveForce, jumpForce,
            dashForce,InterationScale,
            wDamage, HP,
            isGrounded, movement
            );
    }

    private void Start()
    {
        view = GetComponent<PlayerView>();
        view.CreateHp();
        ChangeState(new PlayerIdle(this));
    }

    private void Update()
    {
        Look();
        Vector3 move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        coolDown.Tick(Time.deltaTime);
        model.Movement = move;

        if (Input.GetMouseButtonDown(0) && isGrounded && coolDown.IsReady)
        {
            Debug.Log("공격키 입력");
            ChangeState(new PlayerAttackState(this, currentState));
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("점프 키 입력");
            ChangeState(new PlayerJumpState(this, currentState, rb));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeState(new PlayerDashState(this, currentState, rb));
        }

        Interect();
        view.HPbar(transform.position);
        
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
        Collider[] colls = Physics.OverlapSphere(posInter, model.InterationScale);
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
        view.CheckBox(isFind);
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
    #region 애니메이션
    public void OpenComboWindow()
    {
        if (comboBroken)
            return;

        comboWindowOpen = true;
        comboWindowWasOpened = true;

    }

    public void CloseComboWindow()
    {
        comboWindowOpen = false;
    }

    public void TryContinueFromSword01()
    {
        comboWindowOpen = false;
    }
    #endregion
}
