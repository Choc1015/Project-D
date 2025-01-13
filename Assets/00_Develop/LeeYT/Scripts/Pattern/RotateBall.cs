using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RotateBall : Human
{
    public enum BallType
    {
        피해,
        치유,
        약화,
        강화
    }

    public BallType ballType;
    public float bulletSpeed = 0.5f;
    public GameObject BallSpell;
    private bool isBomb = false;
    private float delayTime = 10;

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        Init();
    }
    private void Start()
    {
        Init();
        StartCoroutine(TimeBomb());
        StartCoroutine(RotateRndWithParticles());
    }

    private void Init()
    {
        bulletSpeed = PatternManager.Instance.BallSpeed;
    }

    private void Update()
    {
        if (!isBomb)
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    IEnumerator RotateRndWithParticles()
    {
        while (true)
        {
            // 랜덤 각도 목표 생성
            float randomAngle = Random.Range(0f, 360f);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, randomAngle);

            // 부드러운 회전 (1초 동안 회전)
            float duration = 1f; // 회전 시간
            float elapsed = 0f;

            // 현재 회전 값을 저장
            Quaternion initialRotation = transform.rotation;

            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            float initialStartRotationZ = ps.main.startRotationZ.constant;// 현재 파티클 StartRotationZ 값
            // 파티클 시스템의 MainModule 가져오기
            var mainModule = ps.main;

            while (elapsed < duration)
            {
                // 비율 계산
                float t = elapsed / duration;

                // Transform 회전 보간
                transform.rotation *= Quaternion.Slerp(initialRotation, targetRotation, t);

                // Transform의 Z축 회전을 Euler 각도로 가져오기
                float zRotation = transform.rotation.eulerAngles.z;

                // 파티클 StartRotationZ 값 적용 (Transform Z축 회전값을 사용)
                //mainModule.startRotationZ = (-(zRotation))  * Mathf.Deg2Rad;

                elapsed += Time.deltaTime;
                yield return null;
            }


            // 최종 회전 값 적용
            transform.rotation = targetRotation;
            mainModule.startRotationZ = randomAngle * Mathf.Deg2Rad; // 라디안 값으로 설정

            // 다음 회전까지 대기
            yield return new WaitForSeconds(1f);
        }
    }



    IEnumerator TimeBomb()
    {
        yield return new WaitForSeconds(delayTime);
        isBomb = true;
        StopAllCoroutines();
        BallSpell.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");

            switch (ballType)
            {
                case BallType.피해:
                    Debug.LogWarning("피해");
                    break;
                case BallType.치유:
                    Debug.LogWarning("치유");
                    break;
                case BallType.약화:
                    Debug.LogWarning("약화");
                    break;
                case BallType.강화:
                    Debug.LogWarning("강화");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
