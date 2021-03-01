using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
	//config
	[SerializeField] private float speed;
	[SerializeField] private float dashSpeed;
	[SerializeField] private float dashCooldown;
	
	//references
	private Rigidbody2D rb;
	private MaskedSlider dashCooldownUI;
	private Timer dashCooldownTimer;
	
	//state
	private Vector2 deltaMovement;
	private bool dash;
	bool canDash = true;
	
	// Awake is called when the script instance is being loaded.
	protected void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		
		dashCooldownUI = FindObjectOfType<MaskedSlider>();
		
		dashCooldownTimer = gameObject.AddComponent<Timer>();
		dashCooldownTimer.Duration = dashCooldown;
		dashCooldownTimer.AddTimerFinishedListener(HandleDashCooldownFinished);
	}
	
	public void Reset(Vector2 position)
	{
		transform.position = position;
		rb.velocity = Vector2.zero;
		canDash = true;
	}
	
	//input
	public void MoveInput(Vector2 input)
	{
		deltaMovement = input;
	}
	
	public void DashInput()
	{
		dash = canDash;
	}

	private void FixedUpdate()
	{
		Move(deltaMovement * speed, dash);
		if (dashCooldownTimer.Running)
		{
			dashCooldownUI.moveSlider(dashCooldownTimer.SecondsLeft / dashCooldown);
		}
	}
	
	private void Move(Vector2 move, bool dash)
	{
		rb.AddForce(move, ForceMode2D.Force);
		if (dash)
		{
			this.dash = false;
			if (move.magnitude == 0)
			{
				//not moving in a direction so no dash
				return;
			}
			this.canDash = false;
			rb.AddForce(move.normalized * dashSpeed, ForceMode2D.Impulse);
			dashCooldownTimer.Run();
		}
	}

	void HandleDashCooldownFinished()
	{
		canDash = true;
	}
}
