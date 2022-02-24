using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public Light flashlightLight;

    private bool LighterActive = false;

    private void Update()
    {
        flashlightLight.gameObject.SetActive(flashlightActive);

        if (batteryCharge <= 0) // only continues code if the battery has charges left
        {
            flashlightActive = false;
            changeBattery();

            return;
        }

        if (!Input.GetMouseButton(0))   // only continues code if the left mouse button is down
        {
            flashlightActive = false;
            return;
        }


        batteryCharge -= batteryUsePerSecond * Time.deltaTime;    // uses charge per seconnd

        if (batteryCharge <= 0)
        {
            batteryCharge = 0;          // minimum of 0% charge for the battery
        }

        flashlightActive = true;
    }


}
