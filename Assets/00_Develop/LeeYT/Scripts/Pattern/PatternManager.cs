using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PatternManager : Singleton<PatternManager>
{

    #region 다크 엘프 패턴 1

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

            // 최소 Intensity 값에 도달하면 정지
            if (GlobalLight.intensity < minIntensity)
            {
                GlobalLight.intensity = minIntensity;
                break;
            }

            yield return null; // 한 프레임 대기
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

    #endregion

    #region 다크 엘프 패턴 2


    [Header("다크 엘프 패턴 2")]
    public bool IsPattern2 = false;

    [SerializeField] List<GameObjectGroup> pattern; // 저장할 위치들
    [System.Serializable]
    public class GameObjectGroup
    {
        public List<Vector2> positions; // GameObject 리스트
    }




    public void SpawnDarkSpell(int num = 0)
    {
        GameObjectGroup group = pattern[num];
        foreach (var spellPos in group.positions)
        {
            Debug.Log("실행");
            ObjectPoolManager.Instance.SpawnFromPool("DarkSpell", spellPos);
        }
    }

    #endregion


    #region 대머리 마법사 기본 공격
    [Header("대머리 마법사 기본 공격")]
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

    #region 대머리 마법사 패턴 1
    [Header("대머리 마법사 패턴 1")]
    public GameObject[] StraightBall;
    public Vector2[] TelePoint;
    public float BallSpeed;

    private int BallCount = 10;

    public void Bomb(Vector3 MaVin)
    {
        float spreadAngle = 360 / BallCount; // 원을 기준으로 터지는 개수에 따른 각도 계산 예) 10개면 36도에 하나씩 터짐

        int randAbility = Random.Range(0, 4); // 랜덤한 능력치

        for (int i = 0; i < BallCount; i++) // for문 돌려서 개수 만큼 반복
        {
            // 오브젝트 풀링 클래스에서 풀로 생성
            GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(StraightBall[randAbility].name, MaVin);
            ParticleSystem ps = bullet.GetComponentInChildren<ParticleSystem>();
            ps.Stop();
            var _main = ps.main;
            _main.startRotationZ = -((spreadAngle * i)-180f) * Mathf.Deg2Rad;// 총알의 회전 값을 총의 회전값에 맞추기
            ps.Play();
            bullet.transform.rotation *= Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, (spreadAngle * i));
        }
    }

    public Vector2 GetRandomTelePoint()
    {
        return TelePoint[Random.Range(0, TelePoint.Length)];
    }

    #endregion

    #region 대머리 마법사 패턴 2
    [Header("대머리 마법사 패턴 2")]
    public GameObject[] RotateBall;
    public float RotateBallSpeed;

    public void FireRotate(Vector3 MaVin)
    {
        int randAbility = Random.Range(0, 4); // 랜덤한 능력치
        int rndCount = Random.Range(2, 5); // 랜덤한 능력치
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