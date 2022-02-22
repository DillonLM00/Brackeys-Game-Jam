using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    //Bewegung
    private float currentMoveSpeed;
    public float moveSpeed = 2;
    public float runningSpeed = 10;

    //Mouse-Look
    public float winkelProSec = 1000f;
    private float nickWinkel = 0f;
    private float gierWinkel = 0f;
    private float minNickWinkel = -20f, maxNickWinkel = 40f;
    private GameObject head;
    private Quaternion ausgangsPoseFigur;
    private Quaternion ausgangsPoseHead;

    //Ausdauer
    public float maxAusdauerInSek = 10;
    private float currentAusdauer;
    public float regenerationsZeit = 10;
    public Image ausdauerAnzeige;

    //Respawn
    private Transform lastCheckpoint;

    //Flashlight
    public Light flashlight;
    public float flashlightSlowDown = 0.65f;

    //----------------------------------------------

    public void setLastCheckpoint(Transform pos)
    {
        lastCheckpoint = pos;
    }

    public void setToCheckpoint()
    {
        transform.position = lastCheckpoint.position;
        transform.rotation = lastCheckpoint.rotation;
    }

    public bool isMoving()
    {
        return Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
    }

    public float getWalkingSpeed()
    {
        return currentMoveSpeed;
    }

    private void MouseLook()
    {
        float horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * winkelProSec;
        float vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * winkelProSec;

        nickWinkel = Mathf.Clamp(nickWinkel - vertical, minNickWinkel, maxNickWinkel);
        gierWinkel += horizontal;

        head.transform.localRotation = Quaternion.AngleAxis(nickWinkel, Vector3.right) * ausgangsPoseHead;
        transform.localRotation = Quaternion.AngleAxis(gierWinkel, Vector3.up) * ausgangsPoseFigur;
    }

    private void Awake()
    {
        lastCheckpoint = transform;

        head = transform.GetComponentInChildren<Camera>().gameObject;
    }

    private void Start()
    {
        ausgangsPoseFigur = transform.localRotation;
        ausgangsPoseHead = head.transform.localRotation;

        currentMoveSpeed = moveSpeed;
        currentAusdauer = maxAusdauerInSek;
    }

    private void Update()
    {
        if (isMoving() && Input.GetKey(KeyCode.LeftShift) && currentAusdauer > 0)   //running
        {
            currentMoveSpeed = runningSpeed;
            currentAusdauer -= Time.deltaTime;
        }
        else                                                                        //Recovery for running
        {
            currentMoveSpeed = moveSpeed;
            currentAusdauer = Mathf.Clamp(currentAusdauer + Time.deltaTime * maxAusdauerInSek / regenerationsZeit, 0, maxAusdauerInSek);
        }
        ausdauerAnzeige.fillAmount = currentAusdauer/maxAusdauerInSek;

        if (flashlight.gameObject.activeSelf)       //if the Flashlight is currently active, player will slow down
        {
            currentMoveSpeed *= flashlightSlowDown;   // 0.75 as a test for slowdown
        }

        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * currentMoveSpeed; //Move forward/backfords
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * currentMoveSpeed; //Move sidewards

        MouseLook();
    }
}
