using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle, // 기본상태
    Skill, // 스킬 사용
    Animation,// 애니메이션
    Stun, // 스턴
    KnockBack, // 넘어짐
    Die // 죽음
}
public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState currentState = PlayerState.Idle;
    public PlayerState CurrentState() => currentState;

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;

    }
}
