using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryDisplay : MonoBehaviour
{
    public GameObject flashlight;
    public TMPro.TextMeshProUGUI batterydisplay;

    private void FixedUpdate()
    {
        if (flashlight.GetComponent<Flashlight>().batteryCharge <= 0)    // tests if the Battery is empty
        {
            batterydisplay.text = "Change Battery";
            return;
        }
                // int Cast to get a clean number for the battery display, shouldnt cause any problems
        batterydisplay.text = ((int)flashlight.GetComponent<Flashlight>().batteryCharge).ToString() + "% Battery";
    }

}
