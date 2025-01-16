using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle, // �⺻����
    Skill, // ��ų ���
    Animation,// �ִϸ��̼�
    Stun, // ����
    KnockBack, // �Ѿ���
    Die, // ����
    Jump
}
public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState currentState = PlayerState.Idle;
    public PlayerState CurrentState() => currentState;

    public void ChangeState(PlayerState newState)
    {
        if (currentState == PlayerState.Die)
            return;
        currentState = newState;

    }

    public void ResetState() => currentState = PlayerState.Idle;
}
