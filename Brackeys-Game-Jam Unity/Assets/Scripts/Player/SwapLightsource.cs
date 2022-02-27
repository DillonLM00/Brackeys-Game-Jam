using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLightsource : MonoBehaviour
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //flashlight.SetActive(!flashlight.activeSelf);
            //lighter.SetActive(!lighter.activeSelf);

            if (flashlight.activeSelf)
            {
                //playerAnimator.runtimeAnimatorController = flashlightAnimator;
            }
            else
            {
                //playerAnimator.runtimeAnimatorController = lighterAnimator;
            }
        }
    }
}
