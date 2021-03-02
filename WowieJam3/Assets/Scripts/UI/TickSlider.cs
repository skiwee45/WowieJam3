using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickSlider : MonoBehaviour
{
	[SerializeField] private Slider slider;
	
	private float timeElapsed;
	private float totalTime;
	private int timeElapsedInt;
	
	public void StartCycle(int timePerCycle)
	{
		totalTime = timePerCycle;
		timeElapsed = 0;
		timeElapsedInt = 0;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		timeElapsed += Time.deltaTime;
		
		var floor = Mathf.FloorToInt(timeElapsed);
		if (floor > timeElapsedInt)
		{
			timeElapsedInt = floor;
			slider.value = timeElapsedInt;
		}
		
		if (timeElapsedInt >= totalTime)
		{
			//reset cycle
			timeElapsed = 0;
			timeElapsedInt = 0;
		}
	}
}
