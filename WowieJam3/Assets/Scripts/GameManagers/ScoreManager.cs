using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// keep score
/// have methods to add score
/// </summary>
public class ScoreManager : MonoBehaviour
{
	[field: SerializeField]
	public float Score {get; private set;}
	
    // Start is called before the first frame update
	void Awake()
    {
	    Score = 0;
    }

	public void AddScore()
	{
		Score ++;
	}
	
	public void SetScore(float newScore)
	{
		Score = newScore;
	}
}
