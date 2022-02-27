using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigtherEffects : MonoBehaviour
{
    public Animator playerAnimator;
    public ParticleSystem sparks;
    public ParticleSystem flame;
    public Light flameLight;

    public void Sparks()
    {
        sparks.Play();
    }
    public void FlameOn()
    {
        flame.Play();
        flame.gameObject.SetActive(true);
        flameLight.gameObject.SetActive(true);
        playerAnimator.SetBool("LighterIsOn",true);
    }
    public void FlameOff()
    {
        flame.Stop();
        flame.gameObject.SetActive(false);
        flameLight.gameObject.SetActive(false);
        playerAnimator.SetBool("LighterIsOn",false);
    }

}
