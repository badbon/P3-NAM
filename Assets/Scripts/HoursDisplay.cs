using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoursDisplay : MonoBehaviour
{
    public TextMeshProUGUI hoursText;
    public int hours = 0;
    public Slider hoursSlider;
    public int maxHours = 48;

    public void SetHours()
    {
        hours = (int)hoursSlider.value;
        hoursText.text = hours.ToString() + " hour(s)";

    }

    void Start()
    {
        
    }

    void Update()
    {
        SetHours();
    }
}
