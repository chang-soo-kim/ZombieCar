using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CamShake : MonoBehaviour
{
    public float ShakeDuration = 0.3f; // 카메라 흔들림이 지속되는 시간
    public float ShakeAmplitube = 1.2f; // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f; // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f; // 경과시간
    

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera; 
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    

    void Start()
    {
        if(VirtualCamera != null)//예외처리
        {
            
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

            
            
        }
        
    }
    public void camShake() // 좀비 데스 코루틴 함수에서 실행함 경과시간을 0.3초로 만들어줌 
    {

        ShakeElapsedTime = ShakeDuration; 

    }

    void Update()
    {
        
        if (VirtualCamera != null || virtualCameraNoise != null) //예외처리
        {
            if (ShakeElapsedTime > 0)
            {
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitube; // 카메라 흔들기 
                virtualCameraNoise.m_AmplitudeGain = ShakeFrequency; // 카메라 흔들기  왜두개쓰는지 알아보기

                ShakeElapsedTime -= Time.deltaTime; // 경과 시간
            }
            else
            {
                virtualCameraNoise.m_AmplitudeGain = 0f; // 카메라 안흔들림
                ShakeElapsedTime = 0f; // 경과시단 0으로 설정
            }
        }     
    }

   
}
