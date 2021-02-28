using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallShoot : MonoBehaviour
{
	//config
	[SerializeField] private float bulletSpeed;
	[SerializeField] private float shootDirections;
	[SerializeField] private float bulletSpawnRadius;
	[SerializeField] private GameObject bulletPrefab;
	
	//input
	private bool shoot = false;
	
	//references
	private Rigidbody2D rb;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
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
			.Where(x => x.type == BallTickType.shoot).FirstOrDefault();
		tickManager.ballTick.AddListener(ShootInput);
	}

	public void ShootInput()
	{
		shoot = true;
	}
	
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		if (shoot)
		{
			Shoot();
		}
	}
	
	private void Shoot()
	{
		//instantiate bullets in circle around ball
		var degreesPerBullet = 360 / shootDirections;
		var randomVariation = Random.Range(0, 90);
		for (float i = randomVariation; i < randomVariation + 360; i+=degreesPerBullet) 
		{
			var direction = EulerAngleToVector2(i);
			var spawnLocation = direction * bulletSpawnRadius + (Vector2)transform.position;
			var bullet = Instantiate(bulletPrefab, spawnLocation, Quaternion.Euler(new Vector3(0, 0, i - 90)));
			
			//add force
			var force = direction * bulletSpeed;
			bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.Max(force + rb.velocity, force), ForceMode2D.Impulse);
		}
		
		shoot = false;
	}
	
	public static Vector2 EulerAngleToVector2 (float eulerAngle)
	{
		var eulerAngleRadians = eulerAngle * Mathf.Deg2Rad;	
		return new Vector2(Mathf.Cos(eulerAngleRadians), Mathf.Sin(eulerAngleRadians)).normalized;
	}
}
