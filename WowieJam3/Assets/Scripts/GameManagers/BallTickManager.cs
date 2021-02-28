using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallTickManager : MonoBehaviour, IGameManager
{
	//config
	public BallTickType type;
	[SerializeField] private float tickRate = 5;
	
	//events
	public UnityEvent ballTick; 
	
	//references
	private Timer timer;
	
	// Awake is called when the script instance is being loaded.
	protected void Start()
	{
		timer = gameObject.AddComponent<Timer>();
		timer.Duration = tickRate;
		timer.AddTimerFinishedListener(HandleTimerFinished);
		timer.Run();
	}
	
	private void HandleTimerFinished()
	{
		ballTick?.Invoke();
		timer.Run();
	}
	
	public void Reset()
	{
		timer.Run();
	}
}

public enum BallTickType
{
	dash,
	shoot,
	shoot2
}