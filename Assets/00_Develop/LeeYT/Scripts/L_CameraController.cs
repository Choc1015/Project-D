using UnityEngine;

public class L_CameraController : MonoBehaviour
{
    private Transform playerTrans; // �÷��̾��� Transform
    public float FollowDistance = 12.5f; // ���� �Ÿ�
    public float SmoothSpeed = 5f; // �ε巴�� �̵��ϴ� �ӵ�

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

        // ī�޶�� �÷��̾� ������ �Ÿ� ���
        float distance = Vector3.Distance(transform.position, playerTrans.position);

        // �Ÿ��� followDistance�� �ʰ��ϸ� ī�޶� �̵�
        if (distance > FollowDistance)
        {
            Vector3 targetPosition = new Vector3(playerTrans.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, SmoothSpeed * Time.deltaTime);
        }
    }
}