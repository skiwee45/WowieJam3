using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


//float unityevent
[Serializable]
public class FloatEvent : UnityEvent<float>{}

public class PlayerLives : MonoBehaviour
{
	//config
	[SerializeField] private float maxLives;
	public FloatEvent onLoseLife;
	public UnityEvent onDeath;
	
	//stats
	private float currentLives;
	
	// Start is called before the first frame update
	void Awake()
	{
		currentLives = maxLives;
	}
	
	public void LoseLife()
	{
		currentLives --;
		if (currentLives == 0)
		{
			onDeath?.Invoke();
		}
		onLoseLife?.Invoke(currentLives);
	}
}
