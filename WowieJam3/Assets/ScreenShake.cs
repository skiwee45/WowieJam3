using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
	private static ScreenShake _instance;

	public static ScreenShake Instance { get { return _instance; } }


	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}
	
	CinemachineVirtualCamera virtualCamera;
	float shakeTimer;
	
    // Start is called before the first frame update
    void Start()
    {
	    virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

	public void ShakeScreen(float intensity, float duration)
	{
		CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		
		noise.m_AmplitudeGain = intensity;
		shakeTimer = duration;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		shakeTimer -= Time.deltaTime;
		
		if (shakeTimer <= 0)
		{
			CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		
			noise.m_AmplitudeGain = 0f;
		}
	}
}
