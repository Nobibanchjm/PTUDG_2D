using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.ZezoVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput == player.facingDir && player.IsWallDectected())
            return;

        player.isMoving = xInput != 0;

        if (player.isMoving && !player.IsBusy)
        {
            if (Time.time > player.lastFootstepTime + player.footstepDelay && player.IsGroundDectected() && !player.IsWallDectected())
            {
                Ads.clip = player.footstepClips[Random.Range(0, player.footstepClips.Length)];
                Ads.Play();
                player.lastFootstepTime = Time.time;
            }
            stateMachine.ChangeState(player.moveState);
        }

        // Nếu không còn di chuyển, dừng âm thanh
        if (!player.isMoving && Ads.isPlaying )
        {
            Ads.Stop();
        }


    }
}
