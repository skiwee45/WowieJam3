using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallDash : MonoBehaviour
{
	//config
	[SerializeField] private float DashSpeed;
	[SerializeField] private float DashDrag;
	
	//references
	private Rigidbody2D rb;
	
	//input
	private bool dash = false;
	
	//state
	private bool dashing = false;
	
    // Start is called before the first frame update
	void Awake()
    {
	    rb = GetComponent<Rigidbody2D>();
    }
    
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		SetupEvents();
	}
	
	private void SetupEvents()
	{
		BallTickManager tickManager = GameManager.Instance.TryGetComponents<BallTickManager>()
			.Where(x => x.type == BallTickType.dash).FirstOrDefault();
		tickManager.ballTick.AddListener(DashInput);
	}

	public void DashInput()
	{
		dash = true;
	}
	
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		if (dashing)
		{
			HandleDashSlowdown();
		}
		
		if (dash)
		{
			Dash();
		}
	}
	
	private void Dash()
	{
		Vector2 forceAndDirection = rb.velocity.normalized * DashSpeed;
		rb.AddForce(forceAndDirection, ForceMode2D.Impulse);
		
		//setup state
		dash = false;
		HandleDashSlowdown();
	}
	
	private void HandleDashSlowdown()
	{
		if (!dashing)
		{
			//initiate
			dashing = true;
			rb.drag	= DashDrag;
		}
		
		if (rb.velocity.magnitude <= GameManager.Instance.ballInitialForce)
		{
			//stop slowdown
			Debug.Log("Stop Dash" + rb.velocity.magnitude);
			rb.drag = 0;
			dashing = false;
		}
	}
}
