using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    private float currentMoveSpeed;
    public float moveSpeed = 2;    //increase while running
    public float runningSpeed = 10;


    public float winkelProSec = 1000f;   //Drehgeschwindigkeit
    private float nickWinkel = 0f;
    private float gierWinkel = 0f;
    private GameObject head;

    public float maxAusdauerInSek = 10;
    private float currentAusdauer;
    public float regenerationsZeit = 10;
    public Image ausdauerAnzeige;

    private Quaternion ausgangsPoseFigur;
    private Quaternion ausgangsPoseHead;
    
    private float minNickWinkel = -20f, maxNickWinkel = 40f;

    public Transform lastCheckpoint;


    public Light flashlight;

    public void setLastCheckpoint(Transform pos)
    {
        lastCheckpoint = pos;
    }

    public void setToCheckpoint()
    {
        transform.position = lastCheckpoint.position;
        transform.rotation = lastCheckpoint.rotation;
    }

    private void Awake()
    {
        lastCheckpoint = transform;
    }

    private void Start()
    {
        head = transform.GetComponentInChildren<Camera>().gameObject;
        ausgangsPoseFigur = transform.localRotation;
        ausgangsPoseHead = head.transform.localRotation;

        currentMoveSpeed = moveSpeed;

        currentAusdauer = maxAusdauerInSek;
    }

    private bool isMoving()
    {
        return Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;
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

    private void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * currentMoveSpeed; //Move forward/backfords
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * currentMoveSpeed; //Move sidewards

        if (flashlight.gameObject.activeSelf)       // slow walking if the Flashlight is currently active
        {
            currentMoveSpeed = moveSpeed * 0.75f;   // 0.75 as a test for slowdown
            currentAusdauer = Mathf.Clamp(currentAusdauer + Time.deltaTime * maxAusdauerInSek / regenerationsZeit, 0, maxAusdauerInSek);
        }
        else if (isMoving() && Input.GetKey(KeyCode.LeftShift) && currentAusdauer > 0)   //running
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


        MouseLook();
    }
}
