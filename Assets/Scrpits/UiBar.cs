using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    [Tooltip("Bar to show how many resources it has")]
    public Slider Slider;
    [Tooltip("Text of Bar in UI")]
    public TextMeshProUGUI textValue;
    [Tooltip("What color will be used on this Bar.")]
    public Gradient gradient;
    [Tooltip("Image of Bar to increase or decrease")]
    public Image fill;
    //public GameObject effectUi;

    private void Start()
    {
        textValue = GetComponentInChildren<TextMeshProUGUI>();

    }
    public void SetMaxBarValue(float maxValue)
    {
        Slider.maxValue = maxValue;
        Slider.value = maxValue;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetBarValue(float setValue)
    {
        Slider.value = setValue;
        fill.color = gradient.Evaluate(Slider.normalizedValue);
    }
    public void SetBarValue(float setValue, string displayName)
    {
        Slider.value = setValue;
        fill.color = gradient.Evaluate(Slider.normalizedValue);
        textValue.text = displayName +": "+ Mathf.Round(setValue).ToString("F2");

    }
    public void GetAnimationOn(bool lowFuel)
    {
        //effectUi.SetActive(lowFuel);
    }

    void SelectEffect()
    {
        //int i = 0;
        //foreach (Transform effect in transform)
        //{
        //    if (effect.name == "Effect")
        //        return;
        //    i++;
        //}
    }
}