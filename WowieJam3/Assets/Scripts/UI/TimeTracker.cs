using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeTracker : MonoBehaviour
{
	private Timer timer;
	[SerializeField] private TMP_Text timeText;
	
	public UnityEvent OnTimeUp;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		timer = gameObject.AddComponent<Timer>();
		timer.AddTimerFinishedListener(HandleTimerUp);
	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		StartTime(60);
	}
	
	public void StartTime(int seconds)
	{
		timer.Duration = seconds;
		timer.Run();
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		timeText.text = Mathf.Round(timer.SecondsLeft).ToString();
	}
	
	private void HandleTimerUp()
	{
		OnTimeUp?.Invoke();
	}
}
