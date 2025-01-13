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
        if (StageManager.Instance.IsStopCamera == true || StageManager.Instance.IsBoss)
        {
            virtualCamera.Follow = null; // ī�޶� ���� ����
            if(StageManager.Instance.IsBoss)
                GameManager.Instance.SetCameraPos(new Vector3(2.05f, 0, -10));
        }
        else if (StageManager.Instance.IsStopCamera == false)
        {
            virtualCamera.Follow = playerTrans; // �÷��̾ �ٽ� ���󰡵��� ����
        }
    }
}
