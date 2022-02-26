using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDeactivation : MonoBehaviour
{
    public List<OpponentTransform> blackList;
    
    private void Start()
    {
        if(!SpiderInformation.spidersActivated)
        {
            swapToReplacer();
        }
    }

    private void swapToReplacer()
    {
        SpiderPartDeactivation spd;

        for(int i = 0; i < blackList.Count; i++)
        {
            spd = blackList[i].GetComponentInChildren<SpiderPartDeactivation>();
            for (int k = 0; k < spd.parts.Count; k++)
            {
                spd.parts[k].SetActive(false);
            }
        }
    }

    private void swapToNormal()
    {
        SpiderPartDeactivation spd;

        for (int i = 0; i < blackList.Count; i++)
        {
            spd = blackList[i].GetComponentInChildren<SpiderPartDeactivation>();
            for (int k = 0; k < spd.parts.Count; k++)
            {
                spd.parts[k].SetActive(true);
            }
        }
    }
}
