using UnityEngine;
using Cinemachine;

public class L_CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public static Transform playerTrans; // 플레이어의 Transform

    private void Start()
    {
        
    }

    private void Update()
    {
        // 카메라가 멈춰야 하는지 확인
        if (StageManager.Instance.IsStopCamera == true)
        {
            virtualCamera.Follow = null; // 카메라 동작 멈춤
        }
        else if (StageManager.Instance.IsStopCamera == false)
            {
            virtualCamera.Follow = playerTrans; // 플레이어를 다시 따라가도록 설정
        }
    }
}
