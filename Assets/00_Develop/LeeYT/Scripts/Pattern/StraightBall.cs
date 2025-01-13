using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBall : MonoBehaviour
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
    private bool isBomb = false;
    private float delayTime = 1.5f; 

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
        bulletSpeed = Random.Range(0.5f, 1.5f);
        delayTime = Random.Range(3f, 5f);
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
                    Debug.Log("����");
                    break;
                case BallType.ġ��:
                    Debug.Log("ġ��");
                    break;
                case BallType.��ȭ:
                    Debug.Log("��ȭ");
                    break;
                case BallType.��ȭ:
                    Debug.Log("��ȭ");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
