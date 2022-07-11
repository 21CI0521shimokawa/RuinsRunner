using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H_GaugeManager : MonoBehaviour
{
    Slider gauge_;
    public Slider gauge
    {
        get
        {
            return gauge_;
        }
    }

    private void Start()
    {
        gauge_ = GetComponent<Slider>();
    }

    public void GaugeValueChange(float _min, float _max, float _value)
    {
        gauge_.value = Mathf.InverseLerp(_min, _max, _value);
    }
}
