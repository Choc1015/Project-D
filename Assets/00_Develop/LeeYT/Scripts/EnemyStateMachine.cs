using System.Collections;
using UnityEngine;

public class EnemyStateMachine : Human
{

    private Vector3 moveDir;
    // States
    private enum EnemyState 
    {
        Idle, // 기본상태
        Patrol, // 정해진 경로 움직임
        Chase, // 플레이어 따라가기
        Attack,// 공격
        Stun, // 스턴
        KnockBack, // 넘어짐
        Die // 죽음
    }
    private EnemyState currentState;
    private GameObject Player;
    private bool isAlive = true; // 살아있는 판정은 필요하고 


    // References
    public float chaseRange = 5f; // 플레이어와 적의 거리
    public float attackRange = 2f; // 공격 범위
    public float patrolSpeed = 2f; // 이동속도인데 이건 스텟에 있으니 가져오고

    // Patrol points
    public Transform[] patrolPoints; // 소환은 나중에 따로 빼놓고

   

    void Start()
    {
        // Start the state machine
        ChangeState(EnemyState.Idle); // 초기 상태
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
        StartCoroutine(newState.ToString()); // 스테이트 이넘이름과 함수이름 동일하게
    }

    private IEnumerator Idle()
    {
        Debug.Log("Entering Idle State");
        while (currentState == EnemyState.Idle)
        {
            // Check if the player is in range
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseRange) // 일정 범위 이하면 쫒아가게 
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
