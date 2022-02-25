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
        originButton = gameObject.GetComponent<Button>().image;
    }

    private void FixedUpdate()
    {

        timeSinceChange += Time.deltaTime;

        if (timeSinceChange>= 0.25)
        {
            originButton.sprite = spriteChanges[arrayPosition];
            arrayPosition = (arrayPosition + 1) % 4;
            timeSinceChange = 0f;
        }
    }
}
