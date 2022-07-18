using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H_NewGaugeManager : MonoBehaviour
{
    [SerializeField] Image back_;
    [SerializeField] Image fill_;

    Color backInitialColor_;
    public Color backInitialColor
    {
        get
        {
            return backInitialColor_;
        }
    }

    Color fillInitialColor_;
    public Color fillInitialColor
    {
        get
        {
            return fillInitialColor_;
        }
    }

    private void Start()
    {
        if(back_ == null)
        {
            back_ = transform.GetChild(0).gameObject.GetComponent<Image>();
        }
        if (fill_ == null)
        {
            fill_ = transform.GetChild(1).gameObject.GetComponent<Image>();
        }
        backInitialColor_ = back_.color;
        fillInitialColor_ = fill_.color;
    }

    public void GaugeValueChange(float _min, float _max, float _value)
    {
        fill_.fillAmount = Mathf.InverseLerp(_min, _max, _value);
    }
}
