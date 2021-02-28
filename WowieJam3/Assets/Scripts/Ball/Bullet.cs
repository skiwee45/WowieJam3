using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	//config
	[SerializeField] private string PlayerTag;
	
	// Sent when an incoming collider makes contact with this object's collider (2D physics only).
	private void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.CompareTag(PlayerTag))
		{
			collisionInfo.gameObject.GetComponent<PlayerLives>().LoseLife();
			Destroy(gameObject);
		}
		
		HitWall(collisionInfo);
	}
	
	public virtual void HitWall(Collision2D collisionInfo)
	{
		Destroy(gameObject);
	}
}
