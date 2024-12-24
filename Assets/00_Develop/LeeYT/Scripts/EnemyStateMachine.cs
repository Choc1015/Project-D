using System.Collections;
using UnityEngine;

public class EnemyStateMachine : Human
{

    private Vector3 moveDir;
    // States
    private enum EnemyState 
    {
        Idle, // �⺻����
        Patrol, // ������ ��� ������
        Chase, // �÷��̾� ���󰡱�
        Attack,// ����
        Stun, // ����
        KnockBack, // �Ѿ���
        Die // ����
    }
    private EnemyState currentState;
    private GameObject Player;
    private bool isAlive = true; // ����ִ� ������ �ʿ��ϰ� 


    // References
    public float chaseRange = 5f; // �÷��̾�� ���� �Ÿ�
    public float attackRange = 2f; // ���� ����
    public float patrolSpeed = 2f; // �̵��ӵ��ε� �̰� ���ݿ� ������ ��������

    // Patrol points
    public Transform[] patrolPoints; // ��ȯ�� ���߿� ���� ������

   

    void Start()
    {
        // Start the state machine
        ChangeState(EnemyState.Idle); // �ʱ� ����
    }

    private void FindPlayers()
    {
        GameObject[] Players;
        if ((Players = GameObject.FindGameObjectsWithTag("Player")) == null)
            return;

        int playerIndex = Random.Range(0, Players.Length);
        Player = Players[playerIndex];
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
        StopAllCoroutines(); // Stop any running state
        StartCoroutine(newState.ToString()); // ������Ʈ �̳��̸��� �Լ��̸� �����ϰ�
    }

    private IEnumerator Idle()
    {
        Debug.Log("Entering Idle State");
        while (currentState == EnemyState.Idle)
        {
            // Check if the player is in range
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseRange) // ���� ���� ���ϸ� �i�ư��� 
            {
                ChangeState(EnemyState.Chase);
            }
            yield return null;
        }
    }

    private IEnumerator Patrol()
    {
        Debug.Log("Entering Patrol State");
        while (currentState == EnemyState.Patrol)
        {
            
            // Check if the player is in range
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseRange)
            {
                ChangeState(EnemyState.Chase);
            }
            else
            {
                movement.MoveToRigid(Vector3.left, statController.GetStat(StatInfo.MoveSpeed).Value);
            }

            yield return null;
        }
    }

    private IEnumerator Chase()
    {
        Debug.Log("Entering Chase State");
        while (currentState == EnemyState.Chase)
        {
            // Chase the player
            FollowPlayer();

            // Transition to Attack if within attack range
            if (Vector3.Distance(transform.position, Player.transform.position) <= attackRange)
            {
                ChangeState(EnemyState.Attack);
            }
            // Return to Idle if player is out of range
            else if (Vector3.Distance(transform.position, Player.transform.position) > chaseRange)
            {
                ChangeState(EnemyState.Idle);
            }

            yield return null;
        }
    }

    void FollowPlayer()
    {
        moveDir = Player.transform.position - transform.position;

        if (moveDir != null)
            movement.MoveToRigid(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
    }

    private IEnumerator Attack()
    {
        Debug.Log("Entering Attack State");
        while (currentState == EnemyState.Attack)
        {
            // Attack logic
            Debug.Log("Attacking the player!");

            // Transition back to Chase if player is out of attack range
            if (Vector3.Distance(transform.position, Player.transform.position) > attackRange)
            {
                ChangeState(EnemyState.Chase);
            }

            yield return new WaitForSeconds(1f); // Attack cooldown
        }
    }

    private IEnumerator Die()
    {
        Debug.Log("Entering Die State");
        isAlive = false;

        // Play death animation or effects
        Debug.Log("Enemy Died");

        yield return new WaitForSeconds(2f); // Wait before destroying the object
        Destroy(gameObject);
    }

    public void TakeDamage(float attackDamage)
    {
        base.TakeDamage(attackDamage);
        // Simulate death for the example
        if (isAlive)
        {
            ChangeState(EnemyState.Die);
        }
    }
}
