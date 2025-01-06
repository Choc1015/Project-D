using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    [Header("성공1확률")]
    public int persent1 = 10;
    [Header("성공2확률")]
    public int persent2 = 10;


    void RandomPersent()
    {
        int persent = Random.Range(0, 100);

        if (persent < persent1) //  퍼센트
        {
            Debug.Log("패턴 1, 2 중 하나!!");
        }
        else if (persent < persent2 + persent1) 
        {

            Debug.Log("패턴 3, 4 중 하나!!");
        }
        else
        {
            Debug.Log("아무일도 없었습니다!!");
        }

       

    }

}
