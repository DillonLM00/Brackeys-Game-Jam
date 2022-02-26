using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighligtItems : MonoBehaviour
{
    public Image flashlightImage;
    public Image lighterImage;
    private int switchCounter = 0;

    private void Start()
    {
        Color tempColorLighter = lighterImage.color;
        tempColorLighter.a = 0.1f;
        lighterImage.color = tempColorLighter;
    }

    private void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Q))
        {
            switchCounter = (switchCounter + 1) % 2;

            if (switchCounter == 1)
            {
                Color tempColorFlashlight = flashlightImage.color;
                tempColorFlashlight.a = 0.1f;
                flashlightImage.color = tempColorFlashlight;

                Color tempColorLighter = lighterImage.color;
                tempColorLighter.a = 1f;
                lighterImage.color = tempColorLighter;
            }
            else
            {
                Color tempColorFlashlight = flashlightImage.color;
                tempColorFlashlight.a = 1f;
                flashlightImage.color = tempColorFlashlight;

                Color tempColorLighter = lighterImage.color;
                tempColorLighter.a = 0.1f;
                lighterImage.color = tempColorLighter;
            }
        }
    }
}
