using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkSpell : MonoBehaviour
{
    [SerializeField] private float spellDuration = 1f; // DarkSpell이 지속되는 시간
    [SerializeField] private float spellInterval = 0.1f; // DarkSpell이 발동되는 간격
    [SerializeField] private Animator animator;
    [SerializeField] private string animationClipName = "DarkSpell";
    [SerializeField] private float visualDistance = 1f; // 시각적 거리 표시

    private bool isCooldown = false; // 딜레이 상태를 추적하는 변수
    private bool isAnimationFinished = false;
    private bool isSpelling = false;
    float normalizedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpellRoutine());
    }

    private IEnumerator SpellRoutine()
    {
        isSpelling = true;
        float elapsedTime = 0f;

        while (elapsedTime < spellDuration)
        {
            Spell();
            elapsedTime += spellInterval;
            yield return new WaitForSeconds(spellInterval);
        }

        isSpelling = false;
    }

    Vector2 SpellPos()
    {
        Vector2 Pos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        return Pos;
    }

    private IEnumerator Spell()
    {
        Utility.GetPlayer().TakeDamage(1, Utility.GetPlayer(), new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
        Debug.Log("DarkSpell to Player");
        yield return new WaitForSeconds(0.5f); // 1초 딜레이
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(Spell());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(Spell());

    }
    private IEnumerator ApplyDamageWithDelay()
    {
        isCooldown = true; // 딜레이 시작
        

        yield return null;
        //yield return new WaitForSeconds(0f); // 1초 딜레이
        isCooldown = false; // 딜레이 종료
    }


    private void OnDrawGizmos()
    {
        // 시각적으로 거리를 표시하기 위한 직사각형 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(SpellPos(), visualDistance);
    }

    void Update()
    {
        if (!isAnimationFinished)
        {
            CheckAnimationState();
        }
    }

    private void CheckAnimationState()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationClipName))
        {

            normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (normalizedTime >= 0.5f)
            {
                isAnimationFinished = true;
                Debug.Log($"{animationClipName} has finished playing.");
                OnAnimationEnd();
            }
        }
    }

    private void OnAnimationEnd()
    {
        // 애니메이션 종료 후 수행할 작업
        Debug.Log("Animation ended. Execute your logic here.");
        Destroy(gameObject);
    }

    
}
