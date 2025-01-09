using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{

    public float bulletSpeed = 0.5f;
    public GameObject BallSpell;
    public GameObject Bomb;

    private bool isBomb = false;

    private void OnEnable()
    {
        Init();
    }

    void Start()
    {
        Init();
        StartCoroutine(TimeBomb());
    }

    private void Init()
    {
        LookAt2D(Utility.GetPlayerTr());
        Bomb.SetActive(false);
    }

    private void Update()
    {
        if(!isBomb)
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    IEnumerator TimeBomb()
    {
        yield return new WaitForSeconds(1.5f);
        isBomb = true;
        BallSpell.SetActive(false);
        Bomb.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
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
            ObjectPoolManager.Instance.DeSpawnToPool(gameObject);
        }
    }
}
