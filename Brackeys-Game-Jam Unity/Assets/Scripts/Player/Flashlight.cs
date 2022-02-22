using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    public float batteryCharge = 100;
    private float batteryUsePerSecond = 10; // how much charge gets used per second

    private bool flashlightActive = false;
    
    private void Update()
    {
        flashlightLight.gameObject.SetActive(flashlightActive);

        if (!Input.GetMouseButton(0))   // only continues code if the left mouse button is down
        {
            flashlightActive = false;
            return;
        }

        if (batteryCharge <= 0) // only continues code if the battery has charges left
        {
            flashlightActive = false;
            //Debug.Log("Change Battery");
            return;
        }

        batteryCharge -= batteryUsePerSecond * Time.deltaTime;    // uses charge per seconnd

        if(batteryCharge <= 0)      
        {
            batteryCharge = 0;          // minimum of 0% charge for the battery
        }

        flashlightActive = true;
    }

  


}
