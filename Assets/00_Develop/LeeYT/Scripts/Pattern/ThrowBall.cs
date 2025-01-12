using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{

    public float bulletSpeed = 0.5f;
    public GameObject BallSpell;
    public GameObject Bomb;
    public Transform FindPlayer;

    private bool isBomb = false;
    private float delayTime = 1.5f;

    private void Start()
    {
        Init();
        StartCoroutine(TimeBomb());
    }

    private void Init()
    {
        FindPlayer = FindObjectOfType<AnimationTrigger>().gameObject.transform;
        if (FindPlayer == null)
            return;
        LookAt2D(FindPlayer);
        bulletSpeed = Random.Range(0.5f, 1.5f);
        delayTime = Random.Range(3f, 5f);
        Bomb.SetActive(false);
    }

    private void Update()
    {
        if (!isBomb)
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    IEnumerator TimeBomb()
    {

        yield return new WaitForSeconds(delayTime);
        isBomb = true;
        BallSpell.SetActive(false);
        Bomb.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    void LookAt2D(Transform targetTransform)
    {
        if (targetTransform == null)
            return;

        // ��� ���� ���
        Vector3 direction = targetTransform.position - transform.position;

        // ���� ��� Atan2 �� ���� ���ϰ� Rad2Deg�� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // �ű⼭ 90�� ���ֱ� �ֳ��ϸ� ������ �������� �� ����
        angle -= 90;

        // Z�� ȸ���� ����
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹");
            Destroy(gameObject);
        }
    }
}
