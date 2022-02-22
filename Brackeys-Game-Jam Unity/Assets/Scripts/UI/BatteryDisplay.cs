using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryDisplay : MonoBehaviour
{
    public GameObject flashlight;
    public TMPro.TextMeshProUGUI batteryChargeDisplay;
    public TMPro.TextMeshProUGUI execssBatteriesDisplay;

    private void Update()
    {
        execssBatteriesDisplay.text =  (flashlight.GetComponent<Flashlight>().exchangeBatteries).ToString() + " extra Batteries";

        if (flashlight.GetComponent<Flashlight>().batteryCharge <= 0)    // tests if the BatteryCharge is empty
        {          
            batteryChargeDisplay.text = "Change Battery";
            return;
        }
        // int Cast to get a clean number for the battery display, shouldnt cause any problems
        batteryChargeDisplay.text = ((int)flashlight.GetComponent<Flashlight>().batteryCharge).ToString() + "% Battery";
    }

}
