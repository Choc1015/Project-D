using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlayerController> players = new();
    public EnemyStateMachine enemyTemp;
    public ClampPosition clampPos;
    [System.Serializable]
    public class ClampPosition
    {
        public float minX, maxX, minY, maxY;
        public Vector3 GetClampPosition(Transform t)
        {
            float x = Mathf.Clamp(t.position.x, minX, maxX);
            float y = Mathf.Clamp(t.position.y, minY, maxY);
            return (Vector3.right * x) + (Vector3.up * y);
        }
    }

    void Update()
    {

    }
}
