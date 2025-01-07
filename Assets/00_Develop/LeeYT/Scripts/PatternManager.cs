using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PatternManager : Singleton<PatternManager>
{

    [Header("��ũ ���� ���� 1")]
    public Light2D GlobalLight;
    public float dimSpeed = 0.1f; // ��Ӱ� �Ǵ� �ӵ�
    public float minIntensity = 0.0f; // �ּ� Intensity ��
    public Transform Sun;
    public bool IsSunAlive = true;
    public bool isPattern1 = false;
    public Vector2 randomRangeX = new Vector2(-6f, 10f); // X�� �̵� ����
    public Vector2 randomRangeY = new Vector2(-4f, -1f); // Y�� �̵� ����
    public float moveDuration = 1.5f; // �̵� �ð�
    public float delayBetweenMoves = 0.5f; // �̵� �� ��� �ð�

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

            // �ּ� Intensity ���� �����ϸ� ����
            if (GlobalLight.intensity < minIntensity)
            {
                GlobalLight.intensity = minIntensity;
                break;
            }

            yield return null; // �� ������ ���
        }
        yield break;
    }

    private IEnumerator LightCoroutine()
    {

        while (GlobalLight.intensity < maxIntensity)
        {
            GlobalLight.intensity += dimSpeed * Time.deltaTime;

            // �ּ� Intensity ���� �����ϸ� ����
            if (GlobalLight.intensity > maxIntensity)
            {
                GlobalLight.intensity = maxIntensity;
                break;
            }

            yield return null; // �� ������ ���0
        }
        isPattern1 = false;
        yield break; // �� ������ ���

    }

    public void SpawnSun()
    {
        Sun = ObjectPoolManager.Instance.SpawnFromPool(Sun.name, Vector2.zero).transform;
        StartRandomMovement();
    }

    private void StartRandomMovement()
    {
        MoveToRandomPosition(); // ù �̵� ����
    }

    private void MoveToRandomPosition()
    {
        // ���� ��ġ ���
        Vector3 randomPosition = new Vector3(
            Random.Range(randomRangeX.x, randomRangeX.y),
            Random.Range(randomRangeY.x, randomRangeY.y),
            Sun.position.z // Z�� ����
        );

        // DoTween���� �̵�
        Sun.DOMove(randomPosition, moveDuration)
            .SetEase(Ease.InOutSine) // �ε巯�� �̵��� ���� Ease ����
            .OnComplete(() =>
            {
                // �̵� �Ϸ� �� ���� �ð� �� �ٽ� ����
                Invoke(nameof(MoveToRandomPosition), delayBetweenMoves);
            });
    }

}   