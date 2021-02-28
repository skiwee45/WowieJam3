using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{
	//config
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private List<GameObject> ballPrefabs;
	[SerializeField] private Vector2 ballSpawnXRange;
	[SerializeField] private Vector2 ballSpawnYRange;
	[SerializeField] private float ballStartingInitialForce;
	[SerializeField] private float ballInitialForceIncrement;
	
	//state
	private float ballInitialForce;
	
	//cached GameObjects
	public PlayerMovement Player = null;
	public List<Ball> Balls;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		ballInitialForce = ballStartingInitialForce;
		CacheObjects();
		ResetBalls();
	}
	
	private void CacheObjects()
	{
		Player = GameObject.FindObjectOfType<PlayerMovement>();
		Balls = GameObject.FindObjectsOfType<Ball>().ToList<Ball>();
	}
	
	//player
	public void SpawnPlayer(Vector2 position)
	{
		Debug.Log("Spawn");
		if (Player != null)
		{
			Player.Reset(position);
			return;
		}
		
		Player = Instantiate(playerPrefab, position, Quaternion.identity).GetComponent<PlayerMovement>();
	}
	
	public void SpawnPlayer()
	{
		SpawnPlayer(Vector2.zero);
	}
	
	//ball
	public void SpawnBall(int ballIndex)
	{
		Debug.Log("Spawn Ball");
		
		var newBall = Instantiate(ballPrefabs[ballIndex]).GetComponent<Ball>();
		Balls.Add(newBall);
		newBall.Reset(GetRandomBallPosition(), ballInitialForce);
	}
	
	public void SpawnBall()
	{
		SpawnBall(0);
	}
	
	public void ResetBalls()
	{
		Debug.Log("Reset Balls");
		foreach (var ball in Balls)
		{
			ball.Reset(GetRandomBallPosition(), ballInitialForce);
		}
	}
	
	public void IncrementBallInitialForce()
	{
		ballInitialForce += ballInitialForceIncrement;
	}
	
	
	//helper
	private Vector2 GetRandomBallPosition()
	{
		return new Vector2(Random.Range(ballSpawnXRange.x, ballSpawnXRange.y)
			, Random.Range(ballSpawnYRange.x, ballSpawnYRange.y));
	}
}
