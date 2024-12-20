using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .5f; // Đặt thời gian tối thiểu để nhân vật rời tường trước khi quay lại
        player.SetVolocity(5f * -player.facingDir, player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // Giảm thời gian stateTimer
        if (stateTimer > 0)
        {
            stateTimer -= Time.deltaTime;
            return; // Đợi hết thời gian trước khi kiểm tra trạng thái khác
        }

        // Chuyển về trạng thái đứng yên nếu chạm đất
        if (player.IsGroundDectected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        // Kiểm tra nếu chạm lại tường sau khi nhảy
        if (player.IsWallDectected())
        {
            stateMachine.ChangeState(player.wallSlide);
            return;
        }
    }
}
