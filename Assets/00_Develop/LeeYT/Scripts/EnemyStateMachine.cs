using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

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
    private float tempAttackOffsetX;
    private bool isAttack = false;

    // References
    public float chaseRange = 5f; // 플레이어와 적의 거리
    public float attackRange = 2f; // 공격 범위   
    public Vector3 AttackOffset;
    public float AttackDelay = 1f;
    

    void Start()
    {
        statController.Init();
        attackRange = statController.GetStat(StatInfo.AttakRange).Value;
        FindPlayers();
        // Start the state machine
        ChangeState(EnemyState.Patrol); // 초기 상태
        tempAttackOffsetX = AttackOffset.x;
    }

    private void FindPlayers()
    {
        GameObject[] Players;
        if ((Players = GameObject.FindGameObjectsWithTag("Player")) == null)
            return;

        int playerIndex = Random.Range(0, Players.Length);
        Player = Players[playerIndex];
    }

    private void OnDrawGizmos()
    {
        // 공격 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackHitBox(), attackRange);

        // 플레이어와 적 사이의 연결 선 그리기
        if (Player != null)
        {

            // 플레이어가 범위 내에 있을 때 초록색 선
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseRange)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, chaseRange);
            }

            // 플레이어가 공격범위 내에 있을 때 파란색 선
            if (Vector3.Distance(AttackHitBox(), Player.transform.position) <= attackRange)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(AttackHitBox(), attackRange);
            }
        }
    }

    private Vector3 AttackHitBox()
    {
        return transform.position + AttackOffset;
    }

    private void FlipSprite()
    {
        if (Player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            AttackOffset.x = tempAttackOffsetX;

            Debug.Log("왼쪽");
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            AttackOffset.x = -tempAttackOffsetX;
        }
        
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
        StopAllCoroutines(); // Stop any running state
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
            FlipSprite();
            // Transition to Attack if within attack range
            if (Vector3.Distance(AttackHitBox(), Player.transform.position) <= attackRange)
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
    private IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(0.3f);
        movement.StopMove();
        yield return new WaitForSeconds(0.2f);
        ChangeState(EnemyState.Patrol);
        
        
    }
    protected override void KnockBackHuman(Vector3 dir)
    {
        ChangeState(EnemyState.KnockBack);
        base.KnockBackHuman(dir);
    }
    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(0.5f);
        movement.StopMove();
        yield return new WaitForSeconds(1.5f);
        ChangeState(EnemyState.Patrol);
    }
    protected override void StunHuman(Vector3 dir)
    {
        ChangeState(EnemyState.Stun);
        base.StunHuman(dir);
    }
    void FollowPlayer()
    {

        moveDir = Player.transform.position - transform.position;

        statController.GetStat(StatInfo.MoveSpeed).Value = statController.GetStat(StatInfo.MoveSpeed).GetMaxValue();

        if (moveDir != null)
            movement.MoveToRigid(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
    }

    private IEnumerator Attack()
    {
        Debug.Log("Entering Attack State"); 
        // 추가 딜레이 시간 작업
        
        while (currentState == EnemyState.Attack)
        {
            
            // Attack logic
            Debug.Log($"Attacking the player!");
            // Attack Delay
            
            yield return new WaitForSeconds(AttackDelay);
            isAttack = false;
            if(!isAttack)
            AttakToPlayer(ref isAttack);

            // Transition back to Chase if player is out of attack range
            if (Vector3.Distance(AttackHitBox(), Player.transform.position) > attackRange)
            {
                ChangeState(EnemyState.Chase);
            }
        }
    }   

    private void AttakToPlayer(ref bool isAttack)
    {
        isAttack = true;
        Player.GetComponent<PlayerController>().TakeDamage(statController.GetStat(StatInfo.AttackDamage).Value, this);
        movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
        Debug.LogWarning($"Attak to Player");
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

    public override void TakeDamage(float attackDamage, Human human, string setStateName = "")
    {
        base.TakeDamage(attackDamage, human, setStateName);
        // Simulate death for the example
        if (isAlive)
        {
            //ChangeState(EnemyState.Die);
        }
    }
}
