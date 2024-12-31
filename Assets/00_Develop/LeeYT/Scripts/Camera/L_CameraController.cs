using UnityEngine;
using Cinemachine;

public class L_CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    public Transform playerTrans; // 플레이어의 Transform

    private void Start()
    {
        // Cinemachine Virtual Camera 컴포넌트 가져오기
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTrans = player.transform;
            virtualCamera.Follow = playerTrans; // Virtual Camera가 플레이어를 따라가도록 설정
        }
    }

    private void Update()
    {
        // 카메라가 멈춰야 하는지 확인
        if (StageManager.Instance.IsStopCamera && virtualCamera.Follow != null)
        {
            virtualCamera.Follow = null; // 카메라 동작 멈춤
        }
        else if (!StageManager.Instance.IsStopCamera && playerTrans != null)
        {
            virtualCamera.Follow = playerTrans; // 플레이어를 다시 따라가도록 설정
        }
    }
}
