using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterAnimationBehavior : MonoBehaviour
{
    public Animator lighterAnimator;

    public void Equip()
    {
        lighterAnimator.SetTrigger("Equip");
    }
    public void Idle()
    {
        lighterAnimator.SetTrigger("Idle");
    }
    public void TakeOff()
    {
        lighterAnimator.SetTrigger("Take Off");
    }
}
