using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    RaycastHit hit;

    public void FlashlightOnOffSound()
    {
        AkSoundEngine.PostEvent("flashlight_on", gameObject);
    }

    public void PlayerStep()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 3, out hit))
        {
            if (hit.transform.tag == "Grass")
            {
                AkSoundEngine.PostEvent("PlayerStepGrass", gameObject);
            }
            else if (hit.transform.tag == "Asphalt")
            {
                AkSoundEngine.PostEvent("PlayerStepAsphalt", gameObject);
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
