using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private float shaketimer;
    float startingIntensity;
    float maxShakeTime;
    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float shakeIntensity, float shakeDuration)
    {
        CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin =
        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;

        startingIntensity = shakeIntensity;
        maxShakeTime = shakeDuration;
        shaketimer = shakeDuration;

    }

    private void Update()
    {
        if (shaketimer > 0)
        {
            shaketimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
            Mathf.Lerp(startingIntensity, 0f, 1 - (shaketimer / maxShakeTime));
        }
    }






























    /*
        private Vector3 camPos;
        private float cameraShakeOffset_X, cameraShakeOffset_Y;
        public void shakeCamera(float shakeTime)
        {
            InvokeRepeating("StartCameraShake", 0f, 0.01f);
            Invoke("StopCameraShake", shakeTime);
        }

        public void StartCameraShake(float shakeAmount)
        {
            if (shakeAmount > 0)
            {
                camPos = transform.position;

                cameraShakeOffset_X = Random.value * shakeAmount * 2 - shakeAmount;
                cameraShakeOffset_Y = Random.value * shakeAmount * 2 - shakeAmount;

                camPos.x += cameraShakeOffset_X;
                camPos.y += cameraShakeOffset_Y;

                transform.position = camPos;
            }
        }

        void StopCameraShake()
        {
            CancelInvoke("StartCameraShake");
            transform.localPosition = Vector3.zero;
        }
        */
}
