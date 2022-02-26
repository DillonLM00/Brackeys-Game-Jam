using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryNumberDisplay : MonoBehaviour
{
    public GameObject parentReference;
    private Flashlight flashlightInformation;
    public TextMeshProUGUI numberDisplay;

    private void Start()
    {
        flashlightInformation = parentReference.GetComponent<BatteryDisplay>().flashLight.GetComponent<Flashlight>();
        Debug.Log(flashlightInformation.exchangeBatteries);
        numberDisplay.text = flashlightInformation.exchangeBatteries.ToString() + "yyyyyyyyyyyyyyyyyyyyy";
    }

    private void FixedUpdate()
    {
        //Debug.Log(numberDisplay.text);
        //numberDisplay.SetText(flashlightInformation.exchangeBatteries.ToString() + "x");
    }
}
