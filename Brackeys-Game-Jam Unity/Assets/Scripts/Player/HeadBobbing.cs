using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    private FirstPersonController player;

    private GameObject head;
    private Vector3 idleHeadPos;
    private Vector3 currentHeadPos;

    private float idleTime = 0f;
    private float movingTime = 0f;
    public float smoothingIdle = 0.02f;

    public float bobbingStrengthHorizontal = 0.035f;
    public float bobbingStrengthVertical = 0.035f;
    public float bobFrequencyMultiplier = 4f;

    private void Awake()
    {
        player = GetComponent<FirstPersonController>();
        head = transform.GetComponentInChildren<Camera>().gameObject;
        idleHeadPos = head.transform.position;
        currentHeadPos = idleHeadPos;
    }

    private void Update()
    {
        if (player.isMoving())
        {
            float bobAmplitude = Mathf.Sqrt(player.getWalkingSpeed() / 100f);
            float bobFrequency = movingTime * bobFrequencyMultiplier * Mathf.Sqrt(player.getWalkingSpeed());

            float cosValue = Mathf.Cos(bobFrequency);
            float sinValue = Mathf.Sin(bobFrequency * 2);
            float horizontalOffset = idleHeadPos.x + cosValue * bobbingStrengthHorizontal + bobAmplitude;
            float verticalOffset = idleHeadPos.y + sinValue * bobbingStrengthVertical + bobAmplitude;
            currentHeadPos = new Vector3(horizontalOffset, verticalOffset, 0);

            idleTime = 0f;
            movingTime += Time.deltaTime;
        }
        else
        {
            movingTime = 0f;
            currentHeadPos = Vector3.Lerp(currentHeadPos, idleHeadPos, idleTime * smoothingIdle);
            idleTime += Time.deltaTime;
        }

        head.transform.localPosition = currentHeadPos;
    }
}
