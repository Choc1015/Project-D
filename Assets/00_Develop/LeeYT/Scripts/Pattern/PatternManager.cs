using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PatternManager : Singleton<PatternManager>
{

    #region ��ũ ���� ���� 1

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
        IsSunAlive = true;
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
            if (!IsSunAlive)
                yield break;

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

    #endregion

    #region ��ũ ���� ���� 2


    [Header("��ũ ���� ���� 2")]
    public bool IsPattern2 = false;

    [SerializeField] List<GameObjectGroup> pattern; // ������ ��ġ��
    [System.Serializable]
    public class GameObjectGroup
    {
        public List<Vector2> positions; // GameObject ����Ʈ
    }




    public void SpawnDarkSpell(int num = 0)
    {
        GameObjectGroup group = pattern[num];
        foreach (var spellPos in group.positions)
        {
            Debug.Log("����");
            ObjectPoolManager.Instance.SpawnFromPool("DarkSpell", spellPos);
        }
    }

    #endregion


    #region ��Ӹ� ������ �⺻ ����
    [Header("��Ӹ� ������ �⺻ ����")]
    public GameObject ThrowBall;

    private float time;
    private float delayTime;
    public void SpawnTBall(Vector3 MaVin)
    {
        delayTime = Random.Range(0.5f, 1.5f);

        if (time < delayTime)
            return;
        ObjectPoolManager.Instance.SpawnFromPool(ThrowBall.name, MaVin);
    }

    #endregion

    #region ��Ӹ� ������ ���� 1
    [Header("��Ӹ� ������ ���� 1")]
    public GameObject[] StraightBall;
    public Vector2[] TelePoint;
    public float BallSpeed;

    private int BallCount = 10;

    public void Bomb(Vector3 MaVin)
    {
        float spreadAngle = 360 / BallCount; // ���� �������� ������ ������ ���� ���� ��� ��) 10���� 36���� �ϳ��� ����

        int randAbility = Random.Range(0, 4); // ������ �ɷ�ġ

        for (int i = 0; i < BallCount; i++) // for�� ������ ���� ��ŭ �ݺ�
        {
            // ������Ʈ Ǯ�� Ŭ�������� Ǯ�� ����
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(StraightBall[randAbility].name, MaVin);
            ParticleSystem ps = bullet.GetComponentInChildren<ParticleSystem>();
            ps.Stop();
            var _main = ps.main;
            _main.startRotationZ = -((spreadAngle * i)-180f) * Mathf.Deg2Rad;// �Ѿ��� ȸ�� ���� ���� ȸ������ ���߱�
            ps.Play();
            bullet.transform.rotation *= Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, (spreadAngle * i));
        }
    }

    public Vector2 GetRandomTelePoint()
    {
        return TelePoint[Random.Range(0, TelePoint.Length)];
    }

    #endregion

    #region ��Ӹ� ������ ���� 2
    [Header("��Ӹ� ������ ���� 2")]
    public GameObject[] RotateBall;
    public float RotateBallSpeed;

    public void FireRotate(Vector3 MaVin)
    {
        int randAbility = Random.Range(0, 4); // ������ �ɷ�ġ
        int rndCount = Random.Range(2, 5); // ������ �ɷ�ġ
        StartCoroutine(SpawnRotate(rndCount,randAbility, MaVin));
    }

    IEnumerator SpawnRotate(int count, int Abt, Vector3 MaVin)
    {
        for (int i = 0; i <= count; i++)
        {
            ObjectPoolManager.Instance.SpawnFromPool(RotateBall[Abt].name, MaVin);
            yield return new WaitForSeconds(1);
        }
    }

    #endregion

    private void Update()
    {

        if (ThrowBall == null)
        {
            return;
        }
        time += Time.deltaTime;
    }

}