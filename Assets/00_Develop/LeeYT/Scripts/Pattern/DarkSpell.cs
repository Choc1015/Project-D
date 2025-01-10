using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkSpell : MonoBehaviour
{
    [SerializeField] private float spellDuration = 1f; // DarkSpell�� ���ӵǴ� �ð�
    [SerializeField] private float spellInterval = 0.1f; // DarkSpell�� �ߵ��Ǵ� ����
    [SerializeField] private Animator animator;
    [SerializeField] private string animationClipName = "DarkSpell";
    [SerializeField] private float visualDistance = 1f; // �ð��� �Ÿ� ǥ��

    private bool isCooldown = false; // ������ ���¸� �����ϴ� ����
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
        yield return new WaitForSeconds(0.5f); // 1�� ������
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
        isCooldown = true; // ������ ����
        

        yield return null;
        //yield return new WaitForSeconds(0f); // 1�� ������
        isCooldown = false; // ������ ����
    }


    private void OnDrawGizmos()
    {
        // �ð������� �Ÿ��� ǥ���ϱ� ���� ���簢�� �׸���
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
        // �ִϸ��̼� ���� �� ������ �۾�
        Debug.Log("Animation ended. Execute your logic here.");
        Destroy(gameObject);
    }

    
}
