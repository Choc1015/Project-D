using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyStateMachine : Human
{

    private Vector3 moveDir;
    // States
    private enum EnemyState
    {
        Idle, // ±âº»»óÅÂ
        Patrol, // Á¤ÇØÁø °æ·Î ¿òÁ÷ÀÓ
        Chase, // ÇÃ·¹ÀÌ¾î µû¶ó°¡±â
        Attack,// °ø°İ
        Stun, // ½ºÅÏ
        KnockBack, // ³Ñ¾îÁü
        Die // Á×À½
    }
    private EnemyState currentState;
    private GameObject Player;
    private bool isAlive = true; // »ì¾ÆÀÖ´Â ÆÇÁ¤Àº ÇÊ¿äÇÏ°í 
    private float tempAttackOffsetX;
    private bool isAttack = false;

<<<<<<< Updated upstream
    // References
    public float chaseRange = 5f; // ÇÃ·¹ÀÌ¾î¿Í ÀûÀÇ °Å¸®
    public float attackRange = 2f; // °ø°İ ¹üÀ§   
=======
    public float chaseRange = 5f; // í”Œë ˆì´ì–´ì™€ ì ì˜ ê±°ë¦¬
    public float attackRange = 2f; // ê³µê²© ë²”ìœ„   
>>>>>>> Stashed changes
    public Vector3 AttackOffset;
    public float AttackDelay = 1f;
    public Animator animator;


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        statController.Init();
        attackRange = statController.GetStat(StatInfo.AttakRange).Value;
        FindPlayers();
        // Start the state machine
        ChangeState(EnemyState.Patrol); // ÃÊ±â »óÅÂ
        tempAttackOffsetX = AttackOffset.x;
        if (animator == null)
            Debug.LogError("ì¸ìŠ¤í™í„°ì°½ì— ì• ë‹ˆë©”ì´í„° ì•ˆ ë„£ìœ¼ì‹¬");
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
        // °ø°İ ¹üÀ§ ½Ã°¢È­
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackHitBox(), attackRange);

        // ÇÃ·¹ÀÌ¾î¿Í Àû »çÀÌÀÇ ¿¬°á ¼± ±×¸®±â
        if (Player != null)
        {

            // ÇÃ·¹ÀÌ¾î°¡ ¹üÀ§ ³»¿¡ ÀÖÀ» ¶§ ÃÊ·Ï»ö ¼±
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseRange)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, chaseRange);
            }

            // ÇÃ·¹ÀÌ¾î°¡ °ø°İ¹üÀ§ ³»¿¡ ÀÖÀ» ¶§ ÆÄ¶õ»ö ¼±
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

            Debug.Log("¿ŞÂÊ");
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
        StartCoroutine(newState.ToString()); // ½ºÅ×ÀÌÆ® ÀÌ³ÑÀÌ¸§°ú ÇÔ¼öÀÌ¸§ µ¿ÀÏÇÏ°Ô
    }

    private IEnumerator Idle()
    {
        Debug.Log("Entering Idle State");
        while (currentState == EnemyState.Idle)
        {
            // Check if the player is in range
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseRange) // ÀÏÁ¤ ¹üÀ§ ÀÌÇÏ¸é ¦i¾Æ°¡°Ô 
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
            isAttack = false;
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
        // Ãß°¡ µô·¹ÀÌ ½Ã°£ ÀÛ¾÷
        
        while (currentState == EnemyState.Attack)
        {
            
            // Attack logic
            Debug.Log($"Attacking the player!");
            // Attack Delay
            animator.SetTrigger("Attack");
            movement.MoveToRigid(Vector3.zero, statController.GetStat(StatInfo.MoveSpeed).Value);
            yield return new WaitForSeconds(AttackDelay);
            // Transition back to Chase if player is out of attack range
            if (Vector3.Distance(AttackHitBox(), Player.transform.position) > attackRange)
            {
                ChangeState(EnemyState.Chase);
                isAttack = false;
                break; // ì½”ë£¨í‹´ ì¢…ë£Œ
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
<<<<<<< Updated upstream

    private void AttakToPlayer(ref bool isAttack)
=======
    private IEnumerator KnockBack()
    {
        animator.SetTrigger("Kncokback");
        yield return new WaitForSeconds(info.knockBackTime);
        ChangeState(EnemyState.Stun);
        movement.StopMove();
    }
    private IEnumerator Stun()
    {
        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(info.stunTime);
        ChangeState(EnemyState.Idle);
        movement.StopMove();
        if (this.info.isStun)
            this.info.isStun = false;
    }
    private void AttakToPlayer()
>>>>>>> Stashed changes
    {
        if (Vector3.Distance(AttackHitBox(), Player.transform.position) <= attackRange)
        {
            Player.GetComponent<PlayerController>().TakeDamage(statController.GetStat(StatInfo.AttackDamage).Value, this);
            Debug.LogWarning($"Attak to Player");
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

    public override void TakeDamage(float attackDamage, Human human, string setStateName = "")
    {
        base.TakeDamage(attackDamage, human, setStateName);
        // Simulate death for the example
        if (isAlive)
        {
<<<<<<< Updated upstream
=======
            ChangeState(EnemyState.Stun); // ìŠ¤í„´ìœ¼ë¡œ ë°”ê¿ˆ
>>>>>>> Stashed changes
            //ChangeState(EnemyState.Die);
        }
    }
}
