using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
	private PlayerLives playerLives;
	public List<GameObject> hearts;
    
    
    void Start()
    {
	    playerLives = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLives>();
        
	    playerLives.onLoseLife.AddListener(LoseHeart);
    }

	public void LoseHeart(float temp){
        Destroy(hearts[hearts.Count-1]);
        hearts.Remove(hearts[hearts.Count-1]);
    }
}
