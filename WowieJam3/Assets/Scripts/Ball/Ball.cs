using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// When hit wall, count points
/// When hit player, player lose life
/// </summary>
public class Ball : MonoBehaviour
{
	//config
	[SerializeField] private string wallTag;
	[SerializeField] private string playerTag;
	
	//events
	public UnityEvent OnHitWall;
	
	//references
	private Rigidbody2D rb;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Sent when an incoming collider makes contact with this object's collider (2D physics only).
	protected void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		var otherGameObject = collisionInfo.gameObject;
		Debug.Log(otherGameObject.name);
		if (otherGameObject.CompareTag(wallTag))
		{
			HandleHitWall(collisionInfo);
		} else if (otherGameObject.CompareTag(playerTag))
		{
			HandleHitPlayer(otherGameObject);
		} else
		{
			Debug.Log("Nothing important hit");
		}
	}
	
	public void Reset(Vector2 position, float initialForce)
	{
		transform.position = position;
		
		//reset velocity
		rb.velocity = Vector2.zero;
		
		//random move
		rb.AddForce(position.normalized * initialForce, ForceMode2D.Impulse);
	}
	
	//collision handlers
	private void HandleHitWall(Collision2D collisionInfo)
	{
		OnHitWall?.Invoke();
	}
	
	private void HandleHitPlayer(GameObject player)
	{
		player.GetComponent<PlayerLives>().LoseLife();
	}
}
