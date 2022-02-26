using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitch : MonoBehaviour
{
    private Image originButton;
    public Sprite[] spriteChanges;
    private int arrayPosition = 0;

    private float timeSinceChange = 0f;     // seconds since the last change


    private void Start()
    {
        //originButton = gameObject.GetComponent<Button>().image;
        originButton = gameObject.GetComponent<Image>();
    }

    private void Update()
    {

        timeSinceChange += Time.deltaTime;

        if (timeSinceChange>= 0.5)     // image switches 4 times a second
        {
            originButton.sprite = spriteChanges[arrayPosition];
            arrayPosition = (arrayPosition + 1) % spriteChanges.Length;
            timeSinceChange = 0f;
        }
    }
}
