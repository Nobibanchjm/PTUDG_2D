using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    private int comboCounter;
    private float lastTimeAttack;
    private float comboWindown = 2f;

    public override void Enter()
    {
        base.Enter();
        if (comboCounter >2 || Time.time > lastTimeAttack + comboWindown) 
        {
            comboCounter = 0;
        }
        player.amr.SetInteger("ComboCount",comboCounter);
        stateTimer = .1f; //Làm cho nhân vật đánh chậm hơn 0.1s để tạo quán tính cho nhân vât khi dang chạy mà ra đòn đánh    


        float attackDir = player.facingDir;

        if (xInput !=0) //Giup cho nhân vật chọn hướng của chiêu dễ dàng hơn
        { 
            attackDir = xInput;
        }

        player.SetVolocity(player.attackMovement[comboCounter].x *attackDir, player.attackMovement[comboCounter].y);

    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttack = Time.time;

        player.StartCoroutine("BusyFor", 0.15f);
    }

    public override void Update()
    {
        base.Update();
        if (trigerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (stateTimer < 0) player.ZezoVelocity(); //khi tan cong nhan vat se dung yen
    }
}
