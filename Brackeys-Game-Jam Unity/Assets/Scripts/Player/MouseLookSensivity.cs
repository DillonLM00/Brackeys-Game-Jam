using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLookSensivity : MonoBehaviour
{
    public static float mouseLookSensivity = 1000f;

    public Slider mouseLookSensivitySlider;

    private void Start()
    {
        mouseLookSensivitySlider.value = mouseLookSensivity;
    }

    public void getMouseLookSensivity(Slider slider)
    {
        mouseLookSensivity = slider.value;
    }
}
