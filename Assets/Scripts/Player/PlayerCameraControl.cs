using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField]
    private CinemachineInputAxisController cinemachineInputAxisController;

    public void EnableCameraRotation()
    {
        cinemachineInputAxisController.enabled = true;
    }

    public void DisableCameraRotation()
    {
        cinemachineInputAxisController.enabled = false;
    }
}
