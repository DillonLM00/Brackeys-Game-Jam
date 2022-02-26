using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLightsource : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject lighter;

    private bool flashlightActive = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            flashlight.SetActive(!flashlight.activeSelf);
            lighter.SetActive(!lighter.activeSelf);
        }
    }
}
