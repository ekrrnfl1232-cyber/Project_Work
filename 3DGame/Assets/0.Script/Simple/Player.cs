using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveForce = 4f;

    //[SerializeField] private float jumpForce = 40f;

    //[SerializeField] private float dashForce = 20f;

    //[SerializeField] private float raylength = 1.005f;

    //[SerializeField] private float InterationScale = 2f;

    //[SerializeField] private int wDamage = 10;

    //public GameObject UiCheckBox;

    //[SerializeField] private GameObject hpBG;
    //[SerializeField] private Image hpImg;

    public int hp, maxhp;

    //private bool isGrounded = true;

    private IState currentState;
    private PlayerMoveState moveState;
    LayerMask groundLayer;
    Rigidbody rb;

    private void Awake()
    {
        moveState = new PlayerMoveState(this);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground", "Box");
        hp = maxhp = 100;
        ChangeState(new PlayerIdle(this));
    }

    void Update()
    {
        currentState?.Tick();

    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    //void TakeDamage(int damage)
    //{
    //    ChangeState(new PlayerHitState(this, currentState,damage));
    //}
}
