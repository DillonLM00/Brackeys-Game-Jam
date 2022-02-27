using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSounds : MonoBehaviour
{
    public void BearRun()
    {
        AkSoundEngine.PostEvent("bear_step", gameObject);
    }


}
