using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    public void SetWWiseVolume(float volume) {
        AkSoundEngine.SetOutputVolume(0, volume);
    }

}
