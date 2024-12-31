using UnityEngine;
using Cinemachine;

public class L_CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public Transform playerTrans; // �÷��̾��� Transform

    private void Start()
    {
        // Cinemachine Virtual Camera ������Ʈ ��������
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // �÷��̾� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTrans = player.transform;
            virtualCamera.Follow = playerTrans; // Virtual Camera�� �÷��̾ ���󰡵��� ����
        }
    }

    private void Update()
    {
        // ī�޶� ����� �ϴ��� Ȯ��
        if (StageManager.Instance.IsStopCamera && virtualCamera.Follow != null)
        {
            virtualCamera.Follow = null; // ī�޶� ���� ����
        }
        else if (!StageManager.Instance.IsStopCamera && playerTrans != null)
        {
            virtualCamera.Follow = playerTrans; // �÷��̾ �ٽ� ���󰡵��� ����
        }
    }
}
