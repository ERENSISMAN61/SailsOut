using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private CinemachineFreeLook CinemachineFreeLook;
    private CinemachineBasicMultiChannelPerlin _cbmcp0;
    private CinemachineBasicMultiChannelPerlin _cbmcp1;
    private CinemachineBasicMultiChannelPerlin _cbmcp2;
    private bool _isShooted;
    public float ShakeTime = 0.4f;
    private float ShakeInstensity = 1f;

    private float timer;

    void Awake()
    {
        CinemachineFreeLook = gameObject.GetComponentInChildren<CinemachineFreeLook>();
        _isShooted = gameObject.GetComponentInParent<PlayerHealthControl>().isShotted;

        _cbmcp0 = CinemachineFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp1 = CinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp2 = CinemachineFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cbmcp0.m_AmplitudeGain = 0f;
        _cbmcp1.m_AmplitudeGain = 0f;
        _cbmcp2.m_AmplitudeGain = 0f;
    }

    void ShakeCamera()
    {
        Debug.Log("ShakeCamera");
        CinemachineBasicMultiChannelPerlin cbmcp0 = CinemachineFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin cbmcp1 = CinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin cbmcp2 = CinemachineFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp0.m_AmplitudeGain = ShakeInstensity;
        cbmcp1.m_AmplitudeGain = ShakeInstensity;
        cbmcp2.m_AmplitudeGain = ShakeInstensity;
        timer = ShakeTime;
    }

    void StopCamera()
    {
        Debug.Log("Stop Camera");
        CinemachineBasicMultiChannelPerlin cbmcp0 = CinemachineFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin cbmcp1 = CinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin cbmcp2 = CinemachineFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp0.m_AmplitudeGain = 0f;
        cbmcp1.m_AmplitudeGain = 0f;
        cbmcp2.m_AmplitudeGain = 0f;
        timer = 0;

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        _isShooted = gameObject.GetComponentInChildren<PlayerHealthControl>().isShotted;
        if (_isShooted)
        {
            ShakeCamera();
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopCamera();
            }
        }

    }
   
}
