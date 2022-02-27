using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBehavior : MonoBehaviour
{
    private Animator playerAnimator;
    private bool hasFlashlightSelected = true;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            hasFlashlightSelected = !hasFlashlightSelected;
            playerAnimator.SetTrigger("Take Off Light");          
        }

        if(Input.GetKeyDown(KeyCode.F))//Lichtquelle anschauen
        {
            playerAnimator.SetTrigger("Check Flashlight");
        }

        if(hasFlashlightSelected)//An-Ausschalten Taschenlampe
        {
            if(Input.GetMouseButtonDown(0))
            {
                playerAnimator.SetTrigger("ActivateTrigger");
            }

        }

        if(!hasFlashlightSelected)
        {
            if(Input.GetMouseButtonDown(0))
            {
                playerAnimator.SetTrigger("Lighter On");
            }
        }
    }
}
