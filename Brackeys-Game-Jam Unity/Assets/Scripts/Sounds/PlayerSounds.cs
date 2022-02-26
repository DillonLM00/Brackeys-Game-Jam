using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    RaycastHit hit;

    public void FlashlightOnOffSound()
    {
        AkSoundEngine.PostEvent("FlashlightOnOff", gameObject);
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

    public void BatterySwap()
    {
        AkSoundEngine.PostEvent("BatterySwap", gameObject);
    }
}
