using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBattery : MonoBehaviour
{
    private Flashlight flashlight;

    private void Start()
    {
        flashlight = FindObjectOfType<Flashlight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flashlight.exchangeBatteries++;
            Destroy(this.gameObject);

        }       
    }
}
