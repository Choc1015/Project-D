using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBall : Human
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
    public int Damage = 1;
    public float Health = 0.1f;
    public int Buff = 1;
    public int Nurff = 1;

    private bool isBomb = false;
    private float delayTime = 7.5f; 

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
    }

    private void Init()
    {
        bulletSpeed = PatternManager.Instance.BallSpeed;
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
                    Utility.GetPlayer().TakeDamage(Damage, this, new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
                    Debug.LogWarning("피해");
                    break;
                case BallType.치유:
                    Utility.GetPlayer().Heal(StatInfo.Health, Health);
                    Debug.LogWarning("치유");
                    break;
                case BallType.약화:
                    Utility.GetPlayerStat().BuffStat(StatInfo.AttackDamage, -Nurff);
                    Debug.LogWarning("약화");
                    break;
                case BallType.강화:
                    Utility.GetPlayerStat().BuffStat(StatInfo.AttackDamage, Buff);
                    Debug.LogWarning("강화");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
