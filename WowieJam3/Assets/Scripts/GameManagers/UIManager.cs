using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
	//references
	[SerializeField] private GameObject deathChoices;
	
	public void EnableDeathChoices(bool enable)
	{
		deathChoices.SetActive(enable);
		Time.timeScale = enable ? 0 : 1;
	}
}
