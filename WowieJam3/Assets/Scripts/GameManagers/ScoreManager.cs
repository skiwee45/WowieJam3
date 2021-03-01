using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// keep score
/// have methods to add score
/// </summary>
public class ScoreManager : MonoBehaviour, IGameManager
{
	[SerializeField] private TMP_Text text;
	
	[field: SerializeField]
	public int Score {get; private set;}
	
    // Start is called before the first frame update
	void Awake()
    {
	    Score = 0;
    }

	public void AddScore()
	{
		Score ++;
		text.text = Score.ToString();
	}
	
	public void SetScore(int newScore)
	{
		Score = newScore;
	}
}
