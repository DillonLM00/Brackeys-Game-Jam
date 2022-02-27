using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMusic : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.PostEvent("music_hunting", gameObject);
    }
}
