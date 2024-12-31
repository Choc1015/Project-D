using UnityEngine;

public class L_CameraController : MonoBehaviour
{
    private Transform playerTrans; // 플레이어의 Transform
    public float FollowDistance = 12.5f; // 기준 거리
    public float SmoothSpeed = 5f; // 부드럽게 이동하는 속도

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
    }

    void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        if (StageManager.Instance.IsStopCamera)
            return;
        if (playerTrans == null)
            return;

        // 카메라와 플레이어 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, playerTrans.position);

        // 거리가 followDistance를 초과하면 카메라 이동
        if (distance > FollowDistance)
        {
            Vector3 targetPosition = new Vector3(playerTrans.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, SmoothSpeed * Time.deltaTime);
        }
    }
}