using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet
{
	//config
	[SerializeField] private float maxBounce;
	[SerializeField] private string ignoreBouncesTag;
	
	//state
	private float bounces = 0;
	
	public override void HitWall(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.CompareTag(ignoreBouncesTag))
		{
			return;
		}
		
		bounces ++;
		if (bounces == maxBounce)
		{
			Destroy(gameObject);
		}
	}
}
