using UnityEngine;
using Cinemachine;

public class L_CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public static Transform playerTrans; // �÷��̾��� Transform

    private void Start()
    {
        
    }

    private void Update()
    {
        // ī�޶� ����� �ϴ��� Ȯ��
        if (StageManager.Instance.IsStopCamera == true)
        {
            virtualCamera.Follow = null; // ī�޶� ���� ����
        }
        else if (StageManager.Instance.IsStopCamera == false)
            {
            virtualCamera.Follow = playerTrans; // �÷��̾ �ٽ� ���󰡵��� ����
        }
    }
}
