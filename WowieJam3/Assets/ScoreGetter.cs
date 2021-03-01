using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGetter : MonoBehaviour
{
	[SerializeField] private SceneLoader loader;
	[SerializeField] private TMP_Text text;
	
    // Start is called before the first frame update
    void Start()
	{
		text.text = SceneLoader.Instance.GetScore() + "";
    }
}
