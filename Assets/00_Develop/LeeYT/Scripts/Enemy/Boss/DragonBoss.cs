using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DragonBoss : Human
{
    public enum BossState
    {
        Spawn,
        Idle,
        Punch,
        Slide,
        Breath,
        Die
    }
    protected BossState currentState;
    protected bool isAlive = true; // 살아있어요
    public bool isAttack = false;
    protected bool isPattern = false;
    bool movingRight = true; // 초기 방향
    // References
    public Animator animator;
    public SoundController soundController;
    public CloneLight spriteLight;

    [Header("패턴확률")]
    public int Persent1 = 10;
    public int Persent2 = 10;
    public int Persent3 = 10;

    [Header("속도")]
    public float moveSpeed = 2f; // 이동 속도

    public Vector3 hitOffset;

    private void Start()
    {
        Initialize();

        UIManager.Instance.bossHealthBar.SetHPValue(statController.GetStat(StatInfo.Health).Value, statController.GetStat(StatInfo.Health).GetMaxValue());
        UIManager.Instance.bossHealthBar.gameObject.SetActive(true);
    }

    protected void Initialize()
    {
        statController.Init();
        //FindPlayers();
        // Start the state machine
        ChangeState(BossState.Spawn); // 초기 상태


        if (animator == null)
            Debug.LogError("no animator");


    }
    private void Update()
    {
    }


    public void ChangeState(BossState newState)
    {
        if (!isAlive)
            return;

        currentState = newState;
        StopAllCoroutines(); // Stop any running state
        StartCoroutine(newState.ToString()); // 스테이트 이넘이름과 함수이름 동일하게
    }

    protected IEnumerator Spawn() // 매직 보스는 쫒아가는게 공격입니다.
    {

        Debug.Log("Entering Spawn State");

        while (currentState == BossState.Spawn)
        {
            isAttack = false; 
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName("DragonSpawn") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);
            ChangeState(BossState.Idle);
        }
    }

    protected IEnumerator Idle() // 매직 보스는 쫓아가는 게 공격입니다.
    {
        Debug.Log("Entering Idle State");
        
        if (!isPattern)
        {
            Invoke("RandomPersent", 3);
        }

        float moveDistance = 3.5f; // 이동 거리
        Vector3 startPosition = new Vector3(0.5f,0,0); // 시작 위치 저장
        

        while (currentState == BossState.Idle)
        {
            isPattern = false;
            isAttack = false;
            // 좌우로 움직임 로직
            if (movingRight)
            {
                Debug.Log("움직여!!");
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;

                // 목표 위치 도달 시 방향 전환
                if (transform.position.x >= startPosition.x + moveDistance)
                {
                    movingRight = false;
                }
            }   
            else
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;

                // 목표 위치 도달 시 방향 전환
                if (transform.position.x <= startPosition.x - moveDistance)
                {
                    Debug.Log(transform.position.x);
                    movingRight = true;
                }
            }

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    protected IEnumerator Punch()
    {
        Debug.Log("Entering Punch State");

        animator.SetTrigger("Punch");
        isPattern = true;
        Invoke("OnAttack", 0.5f);

        yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName("DragonPunch") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);
        isAttack = false;
        isPattern = false;
        ChangeState(BossState.Idle);
    }

    private void OnAttack()
    {
        isAttack = true;
    }

    protected IEnumerator Slide()
    {
        Debug.Log("Entering Punch State");

        isPattern = true; 
        Invoke("OnAttack", 0.5f);

        animator.SetTrigger("Slide");
        yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName("DragonSlide") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);
        isAttack = false;
        isPattern = false;
        ChangeState(BossState.Idle);
    }

    protected IEnumerator Breath()
    {
        Debug.Log("Entering Punch State");

        isPattern = true;
        Invoke("OnAttack", 1f);   

        transform.position = new Vector3(0.5f, 0, 0);

        int rndFlip = Random.Range(0, 2);

        if (rndFlip == 0)
        { 
            // 현재 localScale 값 가져오기
            Vector3 currentScale = transform.localScale;

            // X축에 -1 곱하기
            currentScale.x *= -1;

            // 변경된 스케일 적용
            transform.localScale = currentScale;
        }
        

        animator.SetTrigger("Breath");
        yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName("DragonBreath") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f);
        isAttack = false;
        isPattern = false;

        if (rndFlip == 0)
        {
            // 현재 localScale 값 가져오기
            Vector3 currentScale = transform.localScale;

            // X축에 -1 곱하기
            currentScale.x *= -1;

            // 변경된 스케일 적용
            transform.localScale = currentScale;
        }


        ChangeState(BossState.Idle);
    }

    protected IEnumerator Die()
    {
        Debug.Log("Entering Die State");
        CancelInvoke("RandomPersent");
        isAlive = false;
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(5f); // Wait before destroying the object
        Destroy(gameObject);
    }


    public override void TakeDamage(float attackDamage, Human attackHuman, KnockBackInfo info = null)
    {
        if (!isAlive)
            return;
        if (isPattern)
            return;

        PlayerController player = attackHuman as PlayerController;

        if (player.GetPlayerSkill().isCritical)
            UIManager.Instance.hitImage.InvokeActiveGO(0.1f);

        if (this.info != null && this.info.isKnockBack)
            return;
        base.TakeDamage(attackDamage, attackHuman, info);
        player.SpawnHitEffect(transform.position+ hitOffset);
        // Simulate death for the example
        
        if(!isAlive)
        {
            ChangeState(BossState.Die);
            if (StageManager.Instance.WaveEnemyCount > 0)
            {
                StageManager.Instance.WaveEnemyCount--;
            }
        }
        UIManager.Instance.bossHealthBar.SetHPValue(statController.GetStat(StatInfo.Health).Value, statController.GetStat(StatInfo.Health).GetMaxValue());
        soundController?.PlayOneShotSound("Hit");
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
            ChangeState(BossState.Punch);
        }
        else if (persent < Persent2 + Persent1)
        {

            ChangeState(BossState.Slide);
        }
        else if (persent < Persent2 + Persent1 + Persent3)
        {
            ChangeState(BossState.Breath);
        }
        else
        {
            ChangeState(BossState.Idle);
        }



    }
    public bool GetAlive() => isAlive;
}
