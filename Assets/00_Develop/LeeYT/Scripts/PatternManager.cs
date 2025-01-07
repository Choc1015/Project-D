using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PatternManager : Singleton<PatternManager>
{

    [Header("다크 엘프 패턴 1")]
    public Light2D GlobalLight;
    public float dimSpeed = 0.1f; // 어둡게 되는 속도
    public float minIntensity = 0.0f; // 최소 Intensity 값
    public Transform Sun;
    public bool IsSunAlive = true;
    public bool isPattern1 = false;
    public Vector2 randomRangeX = new Vector2(-6f, 10f); // X축 이동 범위
    public Vector2 randomRangeY = new Vector2(-4f, -1f); // Y축 이동 범위
    public float moveDuration = 1.5f; // 이동 시간
    public float delayBetweenMoves = 0.5f; // 이동 간 대기 시간

    float maxIntensity = 1;


    public void StartDarkNight()
    {
        StartCoroutine(DimLightCoroutine());
    }

    public void EndDarkNight()
    {
        StartCoroutine(LightCoroutine());
    }

    private IEnumerator DimLightCoroutine()
    {
        isPattern1 = true;
        Debug.Log(1);
        while (GlobalLight.intensity > minIntensity)
        {
            Debug.Log(2);
            GlobalLight.intensity -= dimSpeed * Time.deltaTime;

            // 최소 Intensity 값에 도달하면 정지
            if (GlobalLight.intensity < minIntensity)
            {
                GlobalLight.intensity = minIntensity;
                break;
            }

            yield return null; // 한 프레임 대기
        }
        yield break;
    }

    private IEnumerator LightCoroutine()
    {

        while (GlobalLight.intensity < maxIntensity)
        {
            GlobalLight.intensity += dimSpeed * Time.deltaTime;

            // 최소 Intensity 값에 도달하면 정지
            if (GlobalLight.intensity > maxIntensity)
            {
                GlobalLight.intensity = maxIntensity;
                break;
            }

            yield return null; // 한 프레임 대기0
        }
        isPattern1 = false;
        yield break; // 한 프레임 대기

    }

    public void SpawnSun()
    {
        Sun = ObjectPoolManager.Instance.SpawnFromPool(Sun.name, Vector2.zero).transform;
        StartRandomMovement();
    }

    private void StartRandomMovement()
    {
        MoveToRandomPosition(); // 첫 이동 시작
    }

    private void MoveToRandomPosition()
    {
        // 랜덤 위치 계산
        Vector3 randomPosition = new Vector3(
            Random.Range(randomRangeX.x, randomRangeX.y),
            Random.Range(randomRangeY.x, randomRangeY.y),
            Sun.position.z // Z축 고정
        );

        // DoTween으로 이동
        Sun.DOMove(randomPosition, moveDuration)
            .SetEase(Ease.InOutSine) // 부드러운 이동을 위해 Ease 설정
            .OnComplete(() =>
            {
                // 이동 완료 후 지연 시간 후 다시 실행
                Invoke(nameof(MoveToRandomPosition), delayBetweenMoves);
            });
    }

}   