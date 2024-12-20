using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;


    protected float xInput;
    protected float yInput;

    protected Rigidbody2D rb;
    protected AudioSource Ads;

    protected float stateTimer;  //Bộ đếm thời gian trạng thái  
    protected bool trigerCalled;

    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }


    public virtual void Enter()
    {
        player.amr.SetBool(animBoolName, true);
        rb = player.rgb;
        Ads = player.ads;
        trigerCalled = false;
    }


    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.amr.SetFloat("yVelocity", rb.velocity.y);

        stateTimer -= Time.deltaTime;

    }


    public virtual void Exit()
    {
        player.amr.SetBool(animBoolName, false);

    }

    public  virtual void AnimationFinishTrigger()
    {
        trigerCalled = true;
    }
}
