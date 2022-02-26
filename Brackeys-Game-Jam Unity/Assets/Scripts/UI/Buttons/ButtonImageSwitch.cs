using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitch : MonoBehaviour
{
    private Image originImage;
    public Sprite[] spriteChanges;
    private int arrayPosition = 0;

    private float timeSinceChange = 0f;     // seconds since the last change


    private void Start()
    {
        //originButton = gameObject.GetComponent<Button>().image;
        originImage = gameObject.GetComponent<Image>();
        originImage.sprite = spriteChanges[spriteChanges.Length - 1];
    }

    private void Update()
    {

        timeSinceChange += Time.unscaledDeltaTime;

        if (timeSinceChange>= 0.25)     // image switches 4 times a second
        {
            originImage.sprite = spriteChanges[arrayPosition];
            arrayPosition = (arrayPosition + 1) % spriteChanges.Length;
            timeSinceChange = 0f;
        }
    }
}
