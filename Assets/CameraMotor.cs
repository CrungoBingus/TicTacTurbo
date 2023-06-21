using UnityEngine;

using Cinemachine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] myCameras;

    CinemachineVirtualCamera currentCamera;


    public static CameraMotor Instance;
    private void Awake()
    {
        Instance = this;

        foreach (CinemachineVirtualCamera m_vc in myCameras)
        {
            m_vc.enabled = false;
        }
        myCameras[0].enabled = true;
    }

    public void SwitchCamera(int m_camera)
    {
        foreach (CinemachineVirtualCamera m_vc in myCameras)
        {
            m_vc.enabled = false;
        }
        myCameras[m_camera].enabled = true;
    }
}