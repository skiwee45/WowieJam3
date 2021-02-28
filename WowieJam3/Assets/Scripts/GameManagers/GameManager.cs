using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{
	//config
	[Header("Prefabs")]
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private List<GameObject> ballPrefabs;
	[SerializeField] private GameObject blockPrefab;
	[Header("Configuration")]
	[SerializeField] private Vector2 ballSpawnXRange;
	[SerializeField] private Vector2 ballSpawnYRange;
	[SerializeField] private float ballStartingInitialForce;
	[SerializeField] private float ballInitialForceIncrement;
	
	//runtime variables
	private float ballInitialForce;
	private Vector2 boxSpawnPosition;
	
	//caches gameobjects
	[Header("Cached Objects")]
	public PlayerMovement Player = null;
	public List<Ball> Balls;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		ballInitialForce = ballStartingInitialForce;
		boxSpawnPosition = Vector2.zero;
		CacheObjects();
		ResetBalls();
	}
	
	private void CacheObjects()
	{
		Player = GameObject.FindObjectOfType<PlayerMovement>();
		Balls = GameObject.FindObjectsOfType<Ball>().ToList<Ball>();
	}
	
	public void HandleDeathAction(DeathActionType action)
	{
		switch (action)
		{
		case DeathActionType.NewBall: 
			SpawnBall(Balls.Count - 1);
			break;
			
		case DeathActionType.FasterBall: 
			IncrementBallInitialForce();
			break;
			
		case DeathActionType.SpawnWall: 
			SpawnBlock();
			break;
			
		default:
			Debug.Log("No action");
			break;
		}
	}
	
	//player
	public void SpawnPlayer(Vector2 position)
	{
		Debug.Log("Spawn");
		
		if (Player != null)
		{
			boxSpawnPosition = Player.transform.position;
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
		SpawnBall(Balls.Count - 1);
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
		Vector2 randomPosition = Vector2.zero;
		
		//make sure balls don't spawn on player
		do {
			randomPosition.x = Random.Range(ballSpawnXRange.x, ballSpawnXRange.y);
			randomPosition.y = Random.Range(ballSpawnYRange.x, ballSpawnYRange.y);
		} while (randomPosition.magnitude <= Player.transform.localScale.magnitude * 2);
		
		return randomPosition;
	}
	
	//wall
	public void SpawnBlock()
	{
		Instantiate(blockPrefab, boxSpawnPosition, Quaternion.identity);
	}
}

public enum DeathActionType
{
	NewBall,
	FasterBall,
	SpawnWall
}
