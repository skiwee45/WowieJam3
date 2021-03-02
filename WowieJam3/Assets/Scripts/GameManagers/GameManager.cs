using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Pixelplacement;

public class GameManager : MonoBehaviour, IGameManager
{
	private static GameManager _instance;

	public static GameManager Instance { get { return _instance; } }


	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
		}
	}
	
	
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
	[SerializeField] private string bulletTag;
	
	//runtime variables
	[HideInInspector]
	public float ballInitialForce;
	private Vector2 boxSpawnPosition;
	
	//caches gameobjects
	[Header("Cached Objects")]
	public PlayerMovement Player = null;
	public List<Ball> Balls;
	
	[Header("Cached GameManager Components")]
	public List<Component> GameManagers;
	
	// Awake is called when the script instance is being loaded.
	protected void Start()
	{
		ballInitialForce = ballStartingInitialForce;
		boxSpawnPosition = Vector2.zero;
		CacheObjects();
		CacheComponents();
		ResetBalls();
	}
	
	private void CacheObjects()
	{
		Player = GameObject.FindObjectOfType<PlayerMovement>();
		Balls = GameObject.FindObjectsOfType<Ball>().ToList<Ball>();
	}
	
	private void CacheComponents()
	{
		GameManagers = GetComponents<IGameManager>().ToList<IGameManager>().ConvertAll(x => x as Component);
	}
	
	public T TryGetComponent<T>() where T : Component
	{
		var item = GameManagers.Find(component => component is T);
		if (item == null)
		{
			item = GetComponent<T>();
			if (item == null)
			{
				item = gameObject.AddComponent<T>();
			}
			GameManagers.Add(item);
		}
		
		return item as T;
	}
	
	public List<T> TryGetComponents<T>() where T : Component
	{
		List<T> item = GameManagers.Where<Component>(component => component is T).ToList<Component>().ConvertAll(x => (T)x);
		if (!item.Any())
		{
			item.Add(TryGetComponent<T>());
		}
		
		return item;
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
		if (ballIndex == 1)
		{
			TryGetComponent<UIManager>().EnableBallTimer(true);
		}
		Balls.Add(newBall);
		newBall.Reset(GetRandomBallPosition(), ballInitialForce);
		newBall.OnHitWall.AddListener(TryGetComponent<ScoreManager>().AddScore);
	}
	
	public void SpawnBall()
	{
		SpawnBall(Balls.Count);
	}
	
	public void ResetBalls()
	{
		TryGetComponent<UIManager>().StartBallTimer();
		//reset balls and bullets
		foreach (var ball in Balls)
		{
			ball.Reset(GetRandomBallPosition(), ballInitialForce);
		}
		
		foreach (var bullet in GameObject.FindGameObjectsWithTag(bulletTag))
		{
			GameObject.Destroy(bullet);
		}
		
		//reset timer
		foreach (var item in TryGetComponents<BallTickManager>())
		{
			item.Reset();
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
