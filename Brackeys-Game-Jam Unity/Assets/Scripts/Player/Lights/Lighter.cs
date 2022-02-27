using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public Light LighterLight;

    private bool lighterActive = false;

    private bool lighterNeedsRestart = false;

    private float breakTime = 5f;       // time it takes for the lighter to break
    private float timeTillBreaks = 5f;   // time in seconds till the Lighter breaks
    
    private float restartTime = 2f;     // the time in seconds the lighter needs for a restart
    private float currentRestartTime = 0f; // the lighter restarts once this is >= restartTime

    private void Start()
    {
        breakTime = Random.Range(2, 10);        //between 2 to 10 seconds before Lighter breaks again
        timeTillBreaks = breakTime;

        restartTime = Random.Range(1, 3);       //between 1 to 3 seconds to retart the lighter
    }

    private void Update()
    {
        LighterLight.gameObject.SetActive(lighterActive);

        if (Time.timeScale == 0)         // cant use lighter, if the game is paused
        {
            return;
        }

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

                breakTime = Random.Range(2, 10);        // between 2 to 10 seconds before Lighter breaks again
                timeTillBreaks = breakTime;

                restartTime = Random.Range(1, 3);       // new random time for the next restart
                currentRestartTime = 0;
            }
        }
    }
}
