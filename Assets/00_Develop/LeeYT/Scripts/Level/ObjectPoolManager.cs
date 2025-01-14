using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable]
    public class Pool // 풀은 프리팹과 풀링될 사이즈를 가지고 있다.
    {
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools; // 리스트에 여러 풀들을 관리한다. 
    private Dictionary<string, Queue<GameObject>> poolDictionary; // 풀을 딕셔너리에서 생성한다. 
    private Dictionary<string, HashSet<GameObject>> activeObjects; // 활성화된 오브젝트 관리

    protected override void Awake()
    {
        Initialize();
        base.Awake();
    }

    /// <summary>
    /// 여러 Pool들을 돌면서 하나의 Pool을 큐리스트로 만들고 게임 오브젝트 생성
    /// 그리고 풀 사이즈 만큼 오브젝트 생성하여 큐안에 저장 
    /// 그리고 그 큐를 딕셔너리에 저장
    /// </summary>
    private void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        activeObjects = new Dictionary<string, HashSet<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            HashSet<GameObject> activeSet = new HashSet<GameObject>();

            GameObject parentObject = new GameObject("@" + pool.prefab.name);

            for (int i = 0; i < pool.size; i++)
            {
                objectPool.Enqueue(CreateNewObject(pool, parentObject.transform));
            }

            poolDictionary.Add(pool.prefab.name, objectPool);
            activeObjects.Add(pool.prefab.name, activeSet);
        }
    }


    /// <summary>
    /// 오브젝트 생성 함수 풀과 부모 오브젝트를 받아서 부모 오브젝트는 그냥 빈 폴더이고 생성해서 부모 오브젝에 정리한다음 비활성화 
    /// </summary>
    /// <param name="pool"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject CreateNewObject(Pool pool, Transform parent)
    {
        GameObject obj = Instantiate(pool.prefab);
        obj.name = pool.prefab.name;
        obj.transform.SetParent(parent);
        parent.transform.SetParent(transform);
        obj.SetActive(false);
        return obj;
    }

    /// <summary>
    /// 오브젝트 스폰 함수 태그와 장소를 받는다.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="spawnPoint"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag, Vector3 spawnPoint)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            Pool pool = pools.Find(p => p.prefab.name == tag);
            if (pool != null)
            {
                poolDictionary[tag].Enqueue(CreateNewObject(pool, GameObject.Find("@" + pool.prefab.name).transform));
            }
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPoint;

        activeObjects[tag].Add(objectToSpawn); // 활성화된 오브젝트로 등록
        return objectToSpawn;
    }


    // 디스폰 함수 리스폰을 했으니 어디선가 이걸로 프리팹 비활성화를 해줘야함
    public void DeSpawnToPool(GameObject obj)
    {
        obj.transform.position = Vector3.zero;

        string tag = obj.name;
        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Enqueue(obj);
            activeObjects[tag].Remove(obj); // 활성화된 오브젝트에서 제거
            obj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No pool exists for object: " + tag);
            Destroy(obj);
        }
    }


    /// <summary>
    /// 모든 활성화된 프리팹을 디스폰하여 풀로 반환
    /// </summary>
    public void DespawnAll()
    {
        foreach (var pair in activeObjects)
        {
            string tag = pair.Key;
            HashSet<GameObject> activeSet = pair.Value;

            // 활성화된 모든 오브젝트를 디스폰
            foreach (var obj in new List<GameObject>(activeSet)) // 리스트로 복사 후 처리
            {
                DeSpawnToPool(obj);
            }
        }

        Debug.Log("모든 활성화된 오브젝트가 디스폰되었습니다.");
    }



}