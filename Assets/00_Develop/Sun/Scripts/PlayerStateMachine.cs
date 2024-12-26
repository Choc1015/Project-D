using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle, // �⺻����
    UseSkill, // ��ų ���
    Attack,// ����
    Stun, // ����
    KnockBack, // �Ѿ���
    Die // ����
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
