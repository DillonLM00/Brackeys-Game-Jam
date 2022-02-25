using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderInformation : MonoBehaviour
{
    public static bool spidersActivated = true;

    private void Start()
    {
        Debug.Log("Spinnen aktiviert: " + spidersActivated);
    }

    public void toggleSpiders()
    {
        spidersActivated = !spidersActivated;
        Debug.Log("Spinnen aktiviert: " + spidersActivated);
    }
}
