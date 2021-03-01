using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using Pixelplacement;

public class SceneLoader : MonoBehaviour
{
	private static SceneLoader _instance;

	public static SceneLoader Instance { get { return _instance; } }

	
	[Header("Scenes")]
	[SerializeField] [Scene] private string Menu;
	[SerializeField] [Scene] private string Game;
	[SerializeField] [Scene] private string GameOver;
	
	public int score;
	
	public int GetScore()
	{
		return score;
	}
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad(transform.gameObject);
		}
	}
	
	public void LoadMenu()
	{
		SceneManager.LoadScene(Menu);
	}
	public void LoadGame()
	{
		SceneManager.LoadScene(Game);
	}
	public void LoadGameOver()
	{
		SceneManager.LoadScene("Score");
	}
	
	public void LoadScene(SceneType scene)
	{
		switch (scene)
		{
		case SceneType.Menu:
			SceneManager.LoadScene(Menu);
			break;
		case SceneType.Game:
			SceneManager.LoadScene(Game);
			break;
		case SceneType.GameOver:
			SceneManager.LoadScene(GameOver);
			break;
		
		default:
			Debug.Log("No scene found");
			break;
		}
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		RecordScore();
	}
	
	private void RecordScore()
	{
		if (SceneManager.GetActiveScene().name != Game)
		{
			return;
		}
		score = GameManager.Instance.TryGetComponent<ScoreManager>().Score;
		
		Debug.Log("recorded score: " + score);
	}
}

public enum SceneType
{
	Menu,
	Game,
	GameOver
}
