using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryDisplay : MonoBehaviour
{
    public GameObject flashLight;
    private Flashlight flashlightInformation;
    
    private Image originImage;
    public Sprite[] batterChargeStates;

    private void Start()
    {
        //originButton = gameObject.GetComponent<Button>().image;
        originImage = gameObject.GetComponent<Image>();

        if (flashLight == null)
        {
            Debug.Log("Pls Link a Flashlight on the IngameUICanvas");
            return;
        }
        flashlightInformation = flashLight.GetComponent<Flashlight>();
    }

    private void FixedUpdate()
    {
        if(flashLight == null)
        {
            Debug.Log("Pls Link a Flashlight on the IngameUICanvas");
            return;
        }

        if(flashlightInformation.batteryCharge == 0)        // empty
        {
            originImage.sprite = batterChargeStates[0];
            return;
        }

        if (flashlightInformation.batteryCharge <= 25)       // 1 Bar
        {
            originImage.sprite = batterChargeStates[1];
            return;
        }

        if (flashlightInformation.batteryCharge <= 50)       // 2 Bar
        {
            originImage.sprite = batterChargeStates[2];
            return;
        }

        if (flashlightInformation.batteryCharge <= 75)       // 3 Bar
        {
            originImage.sprite = batterChargeStates[3];
            return;
        }

        originImage.sprite = batterChargeStates[4];         // 4 bars
       
    }

}
