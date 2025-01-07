using System.Collections;
using UnityEngine;

public class BossBase : Human
{
    public enum BossState
    {
        Chase,
        Attack,
        Stun,
        KnockBack,
        Die,
        Pattern1,
        Pattern2,
        Pattern3
    }
    private Vector3 moveDir;
    protected BossState currentState;
    protected bool isAlive = true; // 살아있어요
    protected float tempAttackOffsetX;
    protected bool isAttack = false;
    protected bool isPattern = false;

    // References
    public float chaseRange = 5f; // 플레이어와 적의 거리
    public float attackRange = 2f; // 공격 범위   
    [SerializeField] Vector3 AttackOffset;
    public float AttackDelay = 1f;
    public Animator animator;
    public SoundController soundController;
    public CloneLight spriteLight;

    [Header("패턴확률")]
    public int Persent1 = 10;
    public int Persent2 = 10;
    public int Persent3 = 10;

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
        ChangeState(BossState.Chase); // 초기 상태
        tempAttackOffsetX = AttackOffset.x;

        if (animator == null)
            Debug.LogError("no animator");
    }
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

    public void ChangeState(BossState newState)
    {
        currentState = newState;
             // Stop any running state
        StartCoroutine(newState.ToString()); // 스테이트 이넘이름과 함수이름 동일하게    
    }

    protected IEnumerator Chase()
    {
        Debug.Log("Entering Chase State");
        if (!isPattern)
        {
            Invoke("RandomPersent", 3);
        }
        while (currentState == BossState.Chase)
        {
            animator.SetTrigger("Idle");
            isAttack = false;
            // Chase the player
            FollowPlayer();
            FlipSprite();
            // Transition to Attack if within attack range (하이 ㅋ)
            if (Vector3.Distance(AttackHitBox(), Utility.GetPlayerTr().position) <= attackRange)
            {
                ChangeState(BossState.Attack);
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

        while (currentState == BossState.Attack)
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
                ChangeState(BossState.Chase);
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

        ChangeState(BossState.Chase);
        movement.StopMove();
        if (this.info.isKnockBack)
            this.info.isKnockBack = false;
    }
    protected IEnumerator Stun()
    {
        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(info.stunTime);
        ChangeState(BossState.KnockBack);
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

    protected IEnumerator Pattern1()
    {
        Debug.Log("Entering Pattern1 State");
        CancelInvoke("RandomPersent");
        isPattern = true; 
        if (isPattern)
        {
            PatternManager.Instance.StartDarkNight();
            PatternManager.Instance.SpawnSun();
            yield return new WaitUntil(() => !PatternManager.Instance.IsSunAlive);
            isPattern = false;
        }
        PatternManager.Instance.EndDarkNight();
        ChangeState(BossState.Chase);
        yield return new WaitForSeconds(10f);
    }

    protected IEnumerator Pattern2()
    {
        Debug.Log("Entering Pattern2 State");
        isPattern = true;
        movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
        yield return new WaitForSeconds(10f);
        isPattern = false;
        ChangeState(BossState.Chase);
        yield return null;
    }
    protected IEnumerator Pattern3()
    {
        Debug.Log("Entering Pattern3 State");
        isPattern = true;
        movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
        yield return new WaitForSeconds(10f);
        isPattern = false;
        ChangeState(BossState.Chase);
        yield return null;
    }


    protected IEnumerator Die()
    {
        Debug.Log("Entering Die State");
        isAlive = false;
        movement.MoveToRigid(Vector3.zero, 0, isAlive);
        animator.SetTrigger("Die");

        // Play death animation or effects
        Debug.Log("Enemy Died");

        yield return new WaitForSeconds(2f); // Wait before destroying the object
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
    }


    public override void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info = null)
    {
        if (!isAlive)
            return;

        if (this.info != null && this.info.isKnockBack)
            return;
        base.TakeDamage(attackDamage, attackHuman, info);
        // Simulate death for the example
        if (isAlive)
        {
            ChangeState(BossState.Stun); // 스턴을 바꿔 놓음
        }
        else
        {
            ChangeState(BossState.Die);
            if (StageManager.Instance.WaveEnemyCount > 0)
            {
                StageManager.Instance.WaveEnemyCount--;
            }
        }
        soundController.PlayOneShotSound("Hit");
    }
    protected override void DieHuman()
    {
        ChangeState(BossState.Die);
    }


    void RandomPersent()
    {
        if (isPattern)
            return;

        int persent = Random.Range(0, 100);

        if (persent < Persent1) //  퍼센트
        {
            ChangeState(BossState.Pattern1);
        }
        else if (persent < Persent2 + Persent1)
        {

            ChangeState(BossState.Pattern2);
        }
        else if (persent < Persent2 + Persent1 + Persent3)
        {
            ChangeState(BossState.Pattern3);
        }
        else
        {
            ChangeState(BossState.Chase);
        }



    }

}
