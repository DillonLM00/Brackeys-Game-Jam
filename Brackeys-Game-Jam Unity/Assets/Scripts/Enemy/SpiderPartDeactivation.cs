using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPartDeactivation : MonoBehaviour
{
    public List<GameObject> parts;

    private bool lastCheck;

    private void Start()
    {
        lastCheck = SpiderInformation.spidersActivated;

        if (!SpiderInformation.spidersActivated)
        {
            toggleparts();
        }
    }

    private void toggleparts()
    {
        if (SpiderInformation.spidersActivated)
        {
            for(int i = 0; i< parts.Count; i++)
            {
                parts[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if(lastCheck != SpiderInformation.spidersActivated)
        {
            toggleparts();
            lastCheck = SpiderInformation.spidersActivated;
        }
    }
}
