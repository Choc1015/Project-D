using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBall : Human
{
    public enum BallType
    {
        ����,
        ġ��,
        ��ȭ,
        ��ȭ
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
            Debug.Log("�÷��̾�� �浹");

            switch (ballType)
            {
                case BallType.����:
                    Utility.GetPlayer().TakeDamage(Damage, this, new KnockBackInfo(Vector3.zero, 100, 0.1f, 0.2f));
                    Debug.LogWarning("����");
                    EffectManager.Instance.PlayHitPupleEffect(Utility.GetPlayerTr().position + Vector3.up * 2);
                    break;
                case BallType.ġ��:
                    Utility.GetPlayer().Heal(StatInfo.Health, Health);
                    EffectManager.Instance.PlayHealEffect(Utility.GetPlayerTr().position + Vector3.up * 2);
                    Debug.LogWarning("ġ��");
                    break;
                case BallType.��ȭ:
                    Utility.GetPlayerStat().BuffStat(StatInfo.AttackDamage, -Nurff);
                    EffectManager.Instance.PlayWeekEffect(Utility.GetPlayerTr().position + Vector3.up * 2);
                    Debug.LogWarning("��ȭ");
                    break;
                case BallType.��ȭ:
                    Utility.GetPlayerStat().BuffStat(StatInfo.AttackDamage, Buff);
                    EffectManager.Instance.PlayStrongEffect(Utility.GetPlayerTr().position + Vector3.up * 2);
                    Debug.LogWarning("��ȭ");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
