using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool IsDamage = false;
    protected BossState currentState;
    protected bool isAlive = true; // 살아있어요
    protected bool isPattern = false;

    // References
    public float chaseRange = 5f; // 플레이어와 적의 거리
    public float attackRange = 2f; // 공격 범위   
    public float AttackDelay = 1f;
    public Animator animator;
    public SoundController soundController;
    public CloneLight spriteLight;
    Vector2 AttackHitBox;
    public GameObject transPos;

    [Header("패턴확률")]
    public int Persent1 = 10;
    public int Persent2 = 10;
    public int Persent3 = 10;

    private void Start()
    {
        Initialize();
        AttackHitBox = transform.position;
    }

    protected void Initialize()
    {
        statController.Init();
        attackRange = statController.GetStat(StatInfo.AttakRange).Value;
        //FindPlayers();
        // Start the state machine
        ChangeState(BossState.Chase); // 초기 상태


        if (animator == null)
            Debug.LogError("no animator");


    }
    private void Update()
    {
        AttackHitBox = transform.position + Vector3.up;
    }
    protected void OnDrawGizmos()
    {
        // 공격 범위 시각화
        Gizmos.color = Color.red;
        AttackHitBox = transform.position + Vector3.up;
        Gizmos.DrawWireSphere(AttackHitBox, attackRange);


        if (Utility.GetPlayerGO() == null)
            return;
        // 플레이어와 적 사이의 연결 선 그리기
        if (Utility.GetPlayerGO() != null)
        {

            // 플레이어가 공격범위 내에 있을 때 파란색 선
            if (Vector2.Distance(AttackHitBox, Utility.GetPlayerTr().position) <= attackRange)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(AttackHitBox, attackRange);
            }
        }

    }

    protected void FlipSprite()
    {
        if (Utility.GetPlayerTr().position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //AttackOffset.x = tempAttackOffsetX;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            //AttackOffset.x = -tempAttackOffsetX;
        }

    }

    public void ChangeState(BossState newState)
    {
        StopAllCoroutines(); // Stop any running state
        if (!isAlive)
            return;
        currentState = newState;
        StartCoroutine(newState.ToString()); // 스테이트 이넘이름과 함수이름 동일하게
    }

    protected IEnumerator Chase()
    {
        animator.SetTrigger("Idle");
        Debug.Log("Entering Chase State");
        if (!isPattern)
        {
            Invoke("RandomPersent", 3);
        }

        while (currentState == BossState.Chase)
        {
            animator.SetTrigger("Idle");
            FollowPlayer();
            FlipSprite();
            // Transition to Attack if within attack range (하이 ㅋ)
            if (Vector2.Distance(AttackHitBox, Utility.GetPlayerTr().position) <= attackRange)
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

        while (currentState == BossState.Attack)
        {
            // Attack logic
            Debug.Log($"Attacking the player!");

            // 공격 트리거 활성화
            animator.SetTrigger("Attack");

            // 이동 로직
            movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);

            // 공격 애니메이션이 특정 시점(0.9f 이상)에 도달할 때까지 대기
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);
            IsDamage = true;
            // 공격 실행 여부 확인
            if (Vector2.Distance(AttackHitBox, Utility.GetPlayerTr().position) <= attackRange && IsDamage)
            {
                AttakToPlayer(); // 공격 한 번만 실행

            }
            
                ChangeState(BossState.Chase); // 플레이어가 사거리 밖에 있으면 상태 변경
                yield break;
           
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

        Utility.GetPlayer().TakeDamage(statController.GetStat(StatInfo.AttackDamage).Value, this, new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
        Debug.Log($"Attak to Player");
        IsDamage = false;
    }

    protected IEnumerator Pattern1()
    {
        Debug.Log("Entering Pattern1 State");
        CancelInvoke("RandomPersent");
        isPattern = true;
        PatternManager.Instance.isPattern1 = true;

        while (PatternManager.Instance.IsSunAlive)
        {
            animator.SetTrigger("Idle");
            // Chase the player
            

            // Transition to Attack if within attack range (하이 ㅋ)
            if (Vector2.Distance(AttackHitBox, Utility.GetPlayerTr().position) <= attackRange && !PatternManager.Instance.isPattern1)
            {    // Attack logic
                Debug.Log($"Attacking the player!");

                // 공격 트리거 활성화
                animator.SetTrigger("Attack");

                // 이동 로직
                movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);

                // 공격 애니메이션이 특정 시점(0.9f 이상)에 도달할 때까지 대기
                yield return new WaitUntil(() =>
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);
                IsDamage = true;
                // 공격 실행 여부 확인
                if (Vector2.Distance(AttackHitBox, Utility.GetPlayerTr().position) <= attackRange && IsDamage)
                {
                    AttakToPlayer(); // 공격 한 번만 실행
                }
                yield return null;

            }
            else
            {
                FollowPlayer();
                FlipSprite();
            }

            if (PatternManager.Instance.isPattern1 && PatternManager.Instance.IsSunAlive)
            {
                Debug.Log(" Check Test State");
                PatternManager.Instance.StartDarkNight();
                PatternManager.Instance.SpawnSun();
                PatternManager.Instance.isPattern1 = false;
            }
            

            yield return null;
        }


        PatternManager.Instance.EndDarkNight();
        isPattern = false;
        ChangeState(BossState.Chase);

    }

    protected IEnumerator Pattern2()
    {
        Debug.Log("Entering Pattern2 State");
        CancelInvoke("RandomPersent");
        isPattern = true;
        animator.SetTrigger("Stop");
        transform.position = new Vector2(2.36f, -0.8f);
        movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Skil1");
        yield return new WaitForSeconds(2f);
        PatternManager.Instance.SpawnDarkSpell(0);

        animator.SetTrigger("Stop");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Skil2");
        yield return new WaitForSeconds(2f);
        PatternManager.Instance.SpawnDarkSpell(1);

        animator.SetTrigger("Idle");
        isPattern = false;
        ChangeState(BossState.Chase);
        yield return null;
    }
    protected IEnumerator Pattern3()
    {
        Debug.Log("Entering Pattern3 State");
        CancelInvoke("RandomPersent");
        isPattern = true;
        animator.SetTrigger("Stop");
        transform.position = new Vector2(2.36f, -0.8f);
        movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Skil2");
        yield return new WaitForSeconds(2f);
        PatternManager.Instance.SpawnDarkSpell(1);

        animator.SetTrigger("Stop");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Skil1");
        yield return new WaitForSeconds(2f);
        PatternManager.Instance.SpawnDarkSpell(0);

        animator.SetTrigger("Stop");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Skil1");
        yield return new WaitForSeconds(2f);
        PatternManager.Instance.SpawnDarkSpell(0);

        animator.SetTrigger("Stop");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Skil2");
        yield return new WaitForSeconds(2f);
        PatternManager.Instance.SpawnDarkSpell(1);
        animator.SetTrigger("Idle");
        isPattern = false;
        ChangeState(BossState.Chase);
        yield return null;
    }


    protected IEnumerator Die()
    {
        Debug.Log("Entering Die State");
        CancelInvoke("RandomPersent");
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
        if (isPattern)
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
