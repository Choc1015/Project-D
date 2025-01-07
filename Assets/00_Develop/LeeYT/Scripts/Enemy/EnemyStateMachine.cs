using System.Collections;
using UnityEngine;
using UnityEngine.Playables;


public class EnemyStateMachine : Human
{

    private Vector3 moveDir;
    // States
    public enum EnemyState
    {
        Chase, // 플레이어 따라가기
        Attack,// 공격
        Stun, // 스턴
        KnockBack, // 넘어짐
        Die // 죽음
    }
    protected EnemyState currentState;
    protected bool isAlive = true; // 살아있는 판정은 필요하고 
    protected float tempAttackOffsetX;
    protected bool isAttack = false;

    // References
    public float chaseRange = 5f; // 플레이어와 적의 거리
    public float attackRange = 2f; // 공격 범위   
    [SerializeField] Vector3 AttackOffset;
    public float AttackDelay = 1f;
    public Animator animator;
    public SoundController soundController;
    public CloneLight spriteLight;
       
    private void Start()
    {
        Initialize();
    }
    protected void Initialize()
    {
        statController.Init();
        attackRange = statController.GetStat(StatInfo.AttakRange).Value;
        //FindPlayers();
        // Start the state machine
        ChangeState(EnemyState.Chase); // 초기 상태
        tempAttackOffsetX = AttackOffset.x;

        if (animator == null)
            Debug.LogError("�ν�����â���� �ִϸ����� �߰���");
    }

    //private void FindPlayers()
    //{
    //    GameObject[] Players;
    //    if ((Players = GameObject.FindGameObjectsWithTag("Player")) == null)
    //        return;

    //    int playerIndex = Random.Range(0, Players.Length);
    //    Player = Players[playerIndex];
    //}


    protected void OnDrawGizmos()
    {
        // 공격 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackHitBox(), attackRange);

        // 플레이어와 적 사이의 연결 선 그리기
        if (Utility.GetPlayerGO() != null)
        {
                
            // 플레이어가 범위 내에 있을 때 초록색 선
            if (Vector3.Distance(transform.position, Utility.GetPlayerTr().position) <= chaseRange)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, chaseRange);
            }

            // 플레이어가 공격범위 내에 있을 때 파란색 선
            if (Vector3.Distance(AttackHitBox(), Utility.GetPlayerTr().position) <= attackRange)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(AttackHitBox(), attackRange);
            }
        }
        
    }

    protected Vector3 AttackHitBox()
    {
        return transform.position + AttackOffset;
    }

    protected void FlipSprite()
    {
        if (Utility.GetPlayerTr().position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            AttackOffset.x = tempAttackOffsetX;
        }
        else
        {   
            transform.localScale = new Vector3(1, 1, 1);
            AttackOffset.x = -tempAttackOffsetX;
        }
        
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
        StopAllCoroutines(); // Stop any running state
        StartCoroutine(newState.ToString()); // 스테이트 이넘이름과 함수이름 동일하게
    }

    protected IEnumerator Chase()
    {
        Debug.Log("Entering Chase State");
        while (currentState == EnemyState.Chase)
        {
            animator.SetTrigger("Idle");
            isAttack = false;
            // Chase the player
            FollowPlayer();
            FlipSprite();
            // Transition to Attack if within attack range (하이 ㅋ)
            Debug.Log(Utility.GetPlayerTr().position);
            if (Vector3.Distance(AttackHitBox(), Utility.GetPlayerTr().position) <= attackRange)
            {
                ChangeState(EnemyState.Attack);
            }
            


            yield return null;
        }
    }
    void FollowPlayer()
    {
        moveDir = Utility.GetPlayerTr().position - transform.position;

        statController.GetStat(StatInfo.MoveSpeed).Value = statController.GetStat(StatInfo.MoveSpeed).GetMaxValue();

        if (moveDir != null)
            movement.MoveToRigid(moveDir, statController.GetStat(StatInfo.MoveSpeed).Value);
    }

    bool MaxPosition()
    {
        if (transform.position.y > 9 || transform.position.y < -3) 
        {
            Debug.LogWarning("WWWW");
            return true;
        }
        return false;
    }

    protected IEnumerator Attack()
    {
        Debug.Log("Entering Attack State"); 
        // 추가 딜레이 시간 작업
        
        while (currentState == EnemyState.Attack)
        {
            
            // Attack logic
            Debug.Log($"Attacking the player!");
            // Attack Delay
            animator.SetTrigger("Attack");
           
            movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
            yield return new WaitForSeconds(AttackDelay);
            // Transition back to Chase if player is out of attack range
            if (Vector3.Distance(AttackHitBox(), Utility.GetPlayerTr().position) > attackRange)
            {
                isAttack = false;
                ChangeState(EnemyState.Chase);
                break;
            }
            else
            {
                isAttack = true;
            }

            if (isAttack)
            {
                AttakToPlayer();
            }
           
        }
    }


    protected IEnumerator KnockBack()
    {
        animator.SetTrigger("Kncokback");

        yield return new WaitForSeconds(info.knockBackTime);
        
        ChangeState(EnemyState.Chase);
        movement.StopMove();
        if (this.info.isKnockBack)
            this.info.isKnockBack = false;
    }
    protected IEnumerator Stun()
    {
        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(info.stunTime);
        ChangeState(EnemyState.KnockBack);
        movement.StopMove();
        
    }
    protected void AttakToPlayer()
    {
        if (Vector3.Distance(AttackHitBox(), Utility.GetPlayerTr().position) <= attackRange)
        {
            Utility.GetPlayer().TakeDamage(statController.GetStat(StatInfo.AttackDamage).Value, this, new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
            Debug.LogWarning($"Attak to Player");
        }
    
    }

    protected IEnumerator Die()
    {
        Debug.Log("Entering Die State");
        isAlive = false;
        movement.MoveToRigid(Vector3.zero, 0,isAlive);
        animator.SetTrigger("Die");
        
        // Play death animation or effects
        Debug.Log("Enemy Died");

        yield return new WaitForSeconds(2f); // Wait before destroying the object
        Destroy(gameObject);
    }


    public override void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info = null)
    {
        if (!isAlive)
            return;

        if (this.info !=null&& this.info.isKnockBack)
            return;
        base.TakeDamage(attackDamage, attackHuman, info);
        // Simulate death for the example
        if (isAlive)
        {
            ChangeState(EnemyState.Stun); // 스턴을 바꿔 놓음
        }
        else
        {
            ChangeState(EnemyState.Die);
            if (StageManager.Instance.WaveEnemyCount > 0)
            {
                StageManager.Instance.WaveEnemyCount--;
            }
        }
        soundController.PlayOneShotSound("Hit");
    }
    protected override void DieHuman()
    {
        ChangeState(EnemyState.Die);
    }
}
