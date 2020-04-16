using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    public Slider Slider;
    public Gradient gradient;
    public Image fill;
    public GameObject Animation;

    private void Start()
    {
        Animation.SetActive(false);
    }
    public void SetMaxBarValue(float health)
    {
        Slider.maxValue = health;
        Slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetBarValue(float health)
    {
        Slider.value = health;
        fill.color = gradient.Evaluate(Slider.normalizedValue);
    }
    public void GetAnimationOn(bool lowFuel)
    {
        Animation.SetActive(lowFuel);
    }
}