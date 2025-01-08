using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSpell : MonoBehaviour
{
    [SerializeField] private float spellDuration = 1f; // DarkSpell이 지속되는 시간
    [SerializeField] private float spellInterval = 0.1f; // DarkSpell이 발동되는 간격
    [SerializeField] private Animator animator;
    [SerializeField] private string animationClipName = "DarkSpell";
    [SerializeField] private float visualDistance = 2f; // 시각적 거리 표시

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

    private void Spell()
    {
        if (Vector3.Distance(transform.position, Utility.GetPlayerTr().position) <= visualDistance)
        {
            Utility.GetPlayer().TakeDamage(3, Utility.GetPlayer(), new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
            Debug.LogWarning("DarkSpell to Player");
        }
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

            if (normalizedTime >= 1.0f)
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
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
    }

    private void OnDrawGizmos()
    {
        // 시각적으로 거리를 표시하기 위한 직사각형 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(visualDistance * 2, visualDistance * 2, 0));
    }
}
