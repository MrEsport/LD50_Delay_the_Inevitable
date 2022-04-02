using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillToFloodLevel : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if(slider != null)
            Bar.Instance.OnFloodValueChanged.AddListener(SliderOnValueChanged);
        else
        {
            Bar.Instance.OnFloodValueChanged.AddListener(ScaleOnValueChanged);
            transform.localScale = new Vector2(transform.localScale.x, 0);
        }
    }

    private void SliderOnValueChanged(float floodValue)
    {
        slider.value = floodValue;
    }

    private void ScaleOnValueChanged(float floodValue)
    {
        transform.localScale = new Vector2(transform.localScale.x, floodValue);
    }

    private void RemoveListeners()
    {
        if (slider != null)
            Bar.Instance.OnFloodValueChanged.RemoveListener(SliderOnValueChanged);
        else
            Bar.Instance.OnFloodValueChanged.RemoveListener(ScaleOnValueChanged);
    }

    private void OnDestroy() => RemoveListeners();
}
