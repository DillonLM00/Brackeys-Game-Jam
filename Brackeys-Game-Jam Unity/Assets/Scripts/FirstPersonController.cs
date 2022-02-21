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

    public float maxAusdauerInSek = 10;
    private float currentAusdauer;
    public float regenerationsZeit = 10;
    public Image ausdauerAnzeige;

    private Quaternion ausgangsPoseFigur;
    private Quaternion ausgangsPoseHead;
    
    private float minNickWinkel = -20f, maxNickWinkel = 40f;

    public Transform lastCheckpoint;

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
        ausgangsPoseFigur = transform.localRotation;
        ausgangsPoseHead = transform.GetChild(0).localRotation;

        currentMoveSpeed = moveSpeed;

        currentAusdauer = maxAusdauerInSek;
    }

    private void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * currentMoveSpeed; //Move forward/backfords
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * currentMoveSpeed; //Move sidewards

        if (Input.GetKey(KeyCode.LeftShift) && currentAusdauer > 0)
        {
            currentMoveSpeed = runningSpeed;
            currentAusdauer -= Time.deltaTime;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
            currentAusdauer = Mathf.Clamp(currentAusdauer + Time.deltaTime * maxAusdauerInSek / regenerationsZeit, 0, maxAusdauerInSek);
        }
        ausdauerAnzeige.fillAmount = currentAusdauer/maxAusdauerInSek;
        

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) //Mouse-Look
        {
            float horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * winkelProSec;
            float vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * winkelProSec;

            nickWinkel = Mathf.Clamp(nickWinkel - vertical, minNickWinkel, maxNickWinkel);
            gierWinkel += horizontal;

            transform.GetChild(0).localRotation = Quaternion.AngleAxis(nickWinkel, Vector3.right) * ausgangsPoseHead;
            transform.localRotation = Quaternion.AngleAxis(gierWinkel, Vector3.up) * ausgangsPoseFigur;
        }
    }
}
