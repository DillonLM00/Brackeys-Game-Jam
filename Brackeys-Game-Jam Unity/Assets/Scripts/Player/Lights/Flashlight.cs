using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    public float batteryCharge = 100;
    private float batteryUsePerSecond = 30; // how much charge gets used per second

    public int exchangeBatteries = 3;

    private bool flashlightActive = false;
    
    private void Update()
    {
        flashlightLight.gameObject.SetActive(flashlightActive);

        if(Time.timeScale == 0)         // cant use flashlight, if the game is paused
        {
            return;
        }

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

        if(batteryCharge <= 0)      
        {
            batteryCharge = 0;          // minimum of 0% charge for the battery
        }

        flashlightActive = true;
    }


    public void changeBattery() // switches the battery and reduces the extra batteries 
    {
        if (exchangeBatteries > 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                exchangeBatteries -= 1;
                batteryCharge = 100;
            }
        }
    }
}
