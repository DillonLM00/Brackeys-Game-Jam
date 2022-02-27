using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightsource : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject lighter;
    public AnimatorOverrideController flashlightAnimator;
    public AnimatorOverrideController lighterAnimator;

    public GameObject playerArms;

    private Animator playerAnimator;

    private bool flashlightActive = true;

    private void Start()
    {
        playerAnimator = playerArms.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToLighter()
    {
        playerAnimator.runtimeAnimatorController = lighterAnimator;
        lighter.SetActive(!lighter.activeSelf);
        flashlight.SetActive(!flashlight.activeSelf);
    }
    public void SwitchToFlashlight()
    {
        playerAnimator.runtimeAnimatorController = flashlightAnimator;
        lighter.SetActive(!lighter.activeSelf);    
        flashlight.SetActive(!flashlight.activeSelf);
    }
}
