using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanSounds : MonoBehaviour
{
    public void TrashcanRun()
    {
        AkSoundEngine.PostEvent("TrashcanRun", gameObject);
    }
}
