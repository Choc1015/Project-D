using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{

    public EnemyStateMachine[] EnemyPrefab; // 총알 프리팹
    public int initialPoolSize = 10; // 초기 풀 크기
    private Dictionary<EnemyStateMachine, ObjectPool<EnemyStateMachine>> enemyPools = new(); // 적 프리팹별 객체 풀

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
        // 적 객체를 풀로 반환
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
