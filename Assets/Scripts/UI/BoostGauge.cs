using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostGauge : MonoBehaviour
{

    [SerializeField] private Slider slider;
    
    public void SetMaxBoost(float boost)
    {
        slider.maxValue = boost;
        slider.value = boost;
    }


    public void SetBoost(float boost)
    {
        slider.value = boost;
    }
}
