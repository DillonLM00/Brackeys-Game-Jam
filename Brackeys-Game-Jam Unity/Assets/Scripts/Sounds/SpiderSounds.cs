using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSounds : MonoBehaviour
{
    public void SpiderRun()
    {
        AkSoundEngine.PostEvent("SpiderRun", gameObject);
    }
}
