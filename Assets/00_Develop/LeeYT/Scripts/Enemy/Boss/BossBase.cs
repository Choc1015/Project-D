using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    [Header("����1Ȯ��")]
    public int persent1 = 10;
    [Header("����2Ȯ��")]
    public int persent2 = 10;


    void RandomPersent()
    {
        int persent = Random.Range(0, 100);

        if (persent < persent1) //  �ۼ�Ʈ
        {
            Debug.Log("���� 1, 2 �� �ϳ�!!");
        }
        else if (persent < persent2 + persent1) 
        {

            Debug.Log("���� 3, 4 �� �ϳ�!!");
        }
        else
        {
            Debug.Log("�ƹ��ϵ� �������ϴ�!!");
        }

       

    }

}
