using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUIBar : MonoBehaviour
{
    [SerializeField] RawImage barFill;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxFill(float _maxFill)
    {
        slider.maxValue = _maxFill;
        slider.value = _maxFill;
    }

    public void SetFill(float _newFill)
    {
        if (_newFill < 0) _newFill = 0;
        slider.value = _newFill;
    }
    public void SetColor(Color _color) {
        barFill.color = _color;
    }
}
