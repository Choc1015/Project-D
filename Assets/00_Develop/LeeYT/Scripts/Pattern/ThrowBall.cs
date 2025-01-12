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

        // 대상 방향 계산
        Vector3 direction = targetTransform.position - transform.position;

        // 각도 계산 Atan2 로 라디안 구하고 Rad2Deg로 각도로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 거기서 90도 빼주기 왜냐하면 기준이 오른쪽은 본 기준
        angle -= 90;

        // Z축 회전만 설정
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            Destroy(gameObject);
        }
    }
}
