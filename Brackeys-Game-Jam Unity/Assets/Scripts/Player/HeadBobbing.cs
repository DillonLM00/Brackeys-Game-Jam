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

    public float bobbingStrengthHorizontal = 0.2f;
    public float bobbingStrengthVertical = 0.2f;
    public float bobFrequencyMultiplier = 4f;
    public float speedImpactFreq = 0.15f;
    public float speedImpactStrength = 0.2f;

    private void Awake()
    {
        player = GetComponent<FirstPersonController>();
        head = transform.GetComponentInChildren<Camera>().gameObject;
        idleHeadPos = head.transform.localPosition;
        currentHeadPos = idleHeadPos;
    }

    private void Update()
    {
        if (player.isMoving())
        {
            float bobAmplitude = Mathf.Sqrt(player.getWalkingSpeed() * speedImpactStrength/10f);
            float bobFrequency = movingTime * bobFrequencyMultiplier * Mathf.Sqrt(player.getWalkingSpeed()*speedImpactFreq);

            float cosValue = Mathf.Cos(bobFrequency);
            float sinValue = Mathf.Sin(bobFrequency * 2);
            float horizontalOffset = idleHeadPos.x + cosValue * bobbingStrengthHorizontal * bobAmplitude;
            float verticalOffset = idleHeadPos.y + sinValue * bobbingStrengthVertical * bobAmplitude;
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
