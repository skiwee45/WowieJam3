using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float dashSpeed;
	//[SerializeField] MaskedSlider dashCooldownUI;
	private Rigidbody2D rb;
	private Vector2 movement;
	Timer dashCooldownTimer;
	bool canDash = true;
	CapsuleCollider2D capsuleCollider2D;
	Animator animator;
	ParticleSystem dashEffect;
    
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		capsuleCollider2D = GetComponent<CapsuleCollider2D>();
		dashEffect = GetComponentInChildren<ParticleSystem>();
		animator = GetComponent<Animator>();

		dashCooldownTimer = gameObject.AddComponent<Timer>();
		dashCooldownTimer.Duration = 3f;
		dashCooldownTimer.AddTimerFinishedListener(HandleDashCooldownFinished);
	}

	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
        
		//dashCooldownUI.moveSlider(dashCooldownTimer.SecondsLeft);
	}

	private void FixedUpdate()
	{
		rb.AddForce(movement * (speed * Time.deltaTime), ForceMode2D.Force);

		if(canDash && (Input.GetAxis("Jump") > 0 && (movement.y > 0 || movement.x > 0 || movement.y < 0 || movement.x < 0))){
			canDash = false;
			rb.AddForce(movement * (dashSpeed * Time.deltaTime), ForceMode2D.Impulse);
			dashEffect.Play();
			dashCooldownTimer.Run();
		}
	}

	void HandleDashCooldownFinished(){
		canDash = true;
	}
}
