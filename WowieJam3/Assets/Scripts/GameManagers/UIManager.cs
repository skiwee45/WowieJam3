using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour, IGameManager
{
	//references
	[SerializeField] private GameObject deathChoices;
	[SerializeField] private GameObject ballTimer;
	
	public void EnableDeathChoices(bool enable)
	{
		deathChoices.SetActive(enable);
		Time.timeScale = enable ? 0 : 1;
	}
	
	public void EnableBallTimer(bool enable)
	{
		ballTimer.SetActive(enable);
		if (enable)
		{
			StartBallTimer();
		}
	}
	
	public void StartBallTimer()
	{
		ballTimer.GetComponent<TickSlider>().StartCycle(5);
	}
}
