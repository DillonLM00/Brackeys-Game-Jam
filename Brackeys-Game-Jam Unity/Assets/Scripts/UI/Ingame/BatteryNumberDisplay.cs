using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryNumberDisplay : MonoBehaviour
{
    public GameObject flashLight;
    private Flashlight flashlightInformation;

    public TMPro.TextMeshProUGUI numberDisplay;


    private void Start()
    {
        if (flashLight == null)
        {
            Debug.Log("Pls Link a Flashlight on the IngameUICanvas");
            return;
        }
        flashlightInformation = flashLight.GetComponent<Flashlight>();
    }

    private void Update()
    {
        numberDisplay.text = flashlightInformation.exchangeBatteries.ToString() + "x";
    }
}
