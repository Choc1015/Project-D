using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RotateBall : Human
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
            // ���� ���� ��ǥ ����
            float randomAngle = Random.Range(0f, 360f);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, randomAngle);

            // �ε巯�� ȸ�� (1�� ���� ȸ��)
            float duration = 1f; // ȸ�� �ð�
            float elapsed = 0f;

            // ���� ȸ�� ���� ����
            Quaternion initialRotation = transform.rotation;

            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            float initialStartRotationZ = ps.main.startRotationZ.constant;// ���� ��ƼŬ StartRotationZ ��
            // ��ƼŬ �ý����� MainModule ��������
            var mainModule = ps.main;

            while (elapsed < duration)
            {
                // ���� ���
                float t = elapsed / duration;

                // Transform ȸ�� ����
                transform.rotation *= Quaternion.Slerp(initialRotation, targetRotation, t);

                // Transform�� Z�� ȸ���� Euler ������ ��������
                float zRotation = transform.rotation.eulerAngles.z;

                // ��ƼŬ StartRotationZ �� ���� (Transform Z�� ȸ������ ���)
                //mainModule.startRotationZ = (-(zRotation))  * Mathf.Deg2Rad;

                elapsed += Time.deltaTime;
                yield return null;
            }


            // ���� ȸ�� �� ����
            transform.rotation = targetRotation;
            mainModule.startRotationZ = randomAngle * Mathf.Deg2Rad; // ���� ������ ����

            // ���� ȸ������ ���
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
            Debug.Log("�÷��̾�� �浹");

            switch (ballType)
            {
                case BallType.����:
                    Debug.LogWarning("����");
                    break;
                case BallType.ġ��:
                    Debug.LogWarning("ġ��");
                    break;
                case BallType.��ȭ:
                    Debug.LogWarning("��ȭ");
                    break;
                case BallType.��ȭ:
                    Debug.LogWarning("��ȭ");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
