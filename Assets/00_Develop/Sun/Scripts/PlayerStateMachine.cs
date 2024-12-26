using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle, // 기본상태
    UseSkill, // 스킬 사용
    Attack,// 공격
    Stun, // 스턴
    KnockBack, // 넘어짐
    Die // 죽음
}
public class PlayerStateMachine 
{
    private PlayerState currentState = PlayerState.Idle;
    public PlayerState CurrentState() => currentState;

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;

    }
}
