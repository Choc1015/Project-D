using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{

    public EnemyStateMachine[] EnemyPrefab; // �Ѿ� ������
    public int initialPoolSize = 10; // �ʱ� Ǯ ũ��
    private Dictionary<EnemyStateMachine, ObjectPool<EnemyStateMachine>> enemyPools = new(); // �� �����պ� ��ü Ǯ

    protected override void Start()
    {
        foreach (var enemy in EnemyPrefab)
        {
            enemyPools[enemy] = new ObjectPool<EnemyStateMachine>(enemy, initialPoolSize, transform);
        }
        base.Start();
    }

    public EnemyStateMachine SpawnEnemy(EnemyStateMachine enemyPrefab, Vector3 position)
    {
        enemyPrefab = enemyPools[enemyPrefab].SpawnObject();
        enemyPrefab.transform.position = position;
        return null;
    }

    public void ReturnEnemy(EnemyStateMachine enemyPrefab, EnemyStateMachine enemy)
    {
        // �� ��ü�� Ǯ�� ��ȯ
        if (enemyPools.TryGetValue(enemyPrefab, out var pool))
        {
            pool.DespawnObject(enemy);
        }
        else
        {
            Debug.LogError("Enemy prefab not found in pool.");
        }
    }



}
