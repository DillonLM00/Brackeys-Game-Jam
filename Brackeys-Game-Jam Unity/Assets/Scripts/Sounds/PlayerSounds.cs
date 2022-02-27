using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AK.Wwise.Event grassFootstep;
    public AK.Wwise.Event asphaltFootStep;
    RaycastHit hit;

    public void FlashlightOnOffSound()
    {
        AkSoundEngine.PostEvent("flashlight_on", gameObject);
    }

    public void PlayerStep()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.transform.tag == "Grass")
            {
                grassFootstep.Post(gameObject);
            }
            else if (hit.transform.tag == "Asphalt")
            {
                asphaltFootStep.Post(gameObject);
            }
        }
    }

    public void BatteryPickUp()
    {
        AkSoundEngine.PostEvent("pickup_battery", gameObject);
    }

    public void BatterySwap()
    {
        AkSoundEngine.PostEvent("change_battery", gameObject);
    }

    public void ZipperOn()
    {
        AkSoundEngine.PostEvent("zippo_on", gameObject);
    }
}
