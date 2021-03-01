using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimeEffect : MonoBehaviour
{
	public float seconds;
	public Transform circle;
	private float timeLeft;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		GameManager.Instance.Player.GetComponent<PlayerLives>().onLoseLife.AddListener(Reset);
	}
    
    // Update is called once per frame
    void Update()
	{
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
		{
			timeLeft = seconds;
		}
		
		var percent = timeLeft / seconds;
		var scale = Mathf.Lerp(1, 0, percent);
	    
		circle.localScale = new Vector3(scale, scale, 1);
	}
    
	public void Reset(float temp)
	{
		timeLeft = seconds;
	}
}
