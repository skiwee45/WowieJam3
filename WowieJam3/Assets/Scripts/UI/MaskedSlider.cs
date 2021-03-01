using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskedSlider : MonoBehaviour
{
    [SerializeField] private Image slider;
    private float value;
    [SerializeField] private float conversion;

    public void moveSlider(float value)
    {
	    this.value = value;
	    slider.rectTransform.anchoredPosition = new Vector2(value * conversion, slider.rectTransform.anchoredPosition.y);
    }

    //private void Update()
    //{
    //    slider.rectTransform.anchoredPosition = new Vector2(value * conversion, slider.rectTransform.anchoredPosition.y);
    //}
}
