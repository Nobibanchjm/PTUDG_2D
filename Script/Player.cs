using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    [Header("Move Infor")]
    public float movespeed;
    public float JumpForce;
    [SerializeField] public AudioClip Jump;
    [SerializeField] public AudioClip Dash;

    [Header("Dash Infor")]
    public float dashSpeed; //tốc độ Dash
    public float dashDuration; // Thời gian Dash
    [SerializeField] private float dashCoolDown; 
    private float dashUseTimer;//Thoi gian hoi dash
    public float dashDir {  get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] LayerMask whatIsGround;


    [SerializeField]public AudioClip[] footstepClips;
    public float footstepDelay = 0.5f;

    public float lastFootstepTime;
    public bool isMoving;


    public bool IsBusy {  get; private set; }
    public int facingDir { get; private set; } = 1;//giá trị cho flip nhân vật
    private bool facingRight = true;

    #region Components
    public Animator amr { get; private set; }
    public Rigidbody2D rgb { get; private set; }
    public AudioSource ads { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set;}
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        // Khởi tạo các trạng thái với tham số cần thiết
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallSlide = new PlayerWallSlideState(stateMachine, this, "WallSlise");
        wallJump = new PlayerWallJumpState(stateMachine, this, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
    }

    private void Start()
    {
        amr = GetComponentInChildren<Animator>();
        rgb = GetComponent<Rigidbody2D>(); 
        ads = GetComponent<AudioSource>();

        // Đặt trạng thái ban đầu cho stateMachine
        stateMachine.Initialize(idleState);
    }



    private void Update()
    {
        stateMachine.currentState.Update();
        CheckDashInpunt();

    }
    #region Velocity
    public void ZezoVelocity() => rgb.velocity = new Vector2(0, 0);
    public void SetVolocity(float _xvelocity, float _yvelocity)
    {
        rgb.velocity = new Vector2(_xvelocity, _yvelocity);
        FilpController(_xvelocity);
    }
    #endregion

    #region Collision
    public bool IsGroundDectected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDectected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    #endregion

    public void AnimatrionTrigger() => stateMachine.currentState.AnimationFinishTrigger();



    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }


    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    public void FilpController(float _x)
    {
        if (rgb.velocity.x > 0 && !facingRight) Flip();        // nếu nhân vật di chuyển sang hướng dương và đang quay về bên trái(false) ta quay nhân vật về bên phải và ngược lại
        else if (rgb.velocity.x < 0 && facingRight ) Flip();
    }
    #endregion

    private void CheckDashInpunt()
    {
        if (IsWallDectected())
            return;

        dashUseTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUseTimer < 0)
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            dashUseTimer = dashCoolDown;
            ads.PlayOneShot(Dash);

            if(dashDir == 0) dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;
    }
}