using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVolocity(0f , rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDectected() && player.IsWallDectected()) 
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        player.SetVolocity( player.dashDir * player.dashSpeed, 0f);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }    
    }
}
