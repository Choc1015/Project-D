using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSpell : MonoBehaviour
{
    [SerializeField] private float spellDuration = 1f; // DarkSpell�� ���ӵǴ� �ð�
    [SerializeField] private float spellInterval = 0.1f; // DarkSpell�� �ߵ��Ǵ� ����
    [SerializeField] private Animator animator;
    [SerializeField] private string animationClipName = "DarkSpell";
    [SerializeField] private float visualDistance = 2f; // �ð��� �Ÿ� ǥ��

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
        // �ִϸ��̼� ���� �� ������ �۾�
        Debug.Log("Animation ended. Execute your logic here.");
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
    }

    private void OnDrawGizmos()
    {
        // �ð������� �Ÿ��� ǥ���ϱ� ���� ���簢�� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(visualDistance * 2, visualDistance * 2, 0));
    }
}
