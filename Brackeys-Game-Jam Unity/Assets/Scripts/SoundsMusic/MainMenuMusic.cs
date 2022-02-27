using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.PostEvent("music_calm", gameObject);
    }

}
