using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public Light LighterLight;

    private bool lighterActive = false;

    private bool lighterNeedsRestart = false;

    private const float breakTime = 5f;       // time it takes for the lighter to break
    private float timeTillBreaks = 5f;   // time in seconds till the Lighter breaks
    
    private const float restartTime = 2f;     // the time in seconds the lighter needs for a restart
    private float currentRestartTime = 0f; // the lighter restarts once this is >= restartTime



    private void Update()
    {
        LighterLight.gameObject.SetActive(lighterActive);

        if (lighterNeedsRestart)
        {
            restartLighter();
            lighterActive = false;
            return;
        }

        if (!Input.GetMouseButton(0))   // only continues code if the left mouse button is down
        {
            lighterActive = false;
            return;
        }

        timeTillBreaks -= Time.deltaTime;
        
        if(timeTillBreaks <= 0)
        {
            lighterNeedsRestart = true;
        }
        lighterActive = true;
    }


    private void restartLighter()
    {
        if (Input.GetKey(KeyCode.R))
        {
            currentRestartTime += Time.deltaTime;

            if (currentRestartTime >= restartTime)
            {
                lighterNeedsRestart = false;
                timeTillBreaks = breakTime;
                currentRestartTime = 0;
            }
        }
    }
}
