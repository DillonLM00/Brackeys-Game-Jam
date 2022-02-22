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
    public float speedChangeDuration = 0.3f;
    private float speedChangeTime = 0f;
    public float runRecoveryCooldown = 5f;
    private bool runRecovery = false;

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
    public float regenerationsZeit = 15;
    public Image ausdauerAnzeige;

    //Respawn
    private Transform lastCheckpoint;

    //Flashlight
    public Light flashlight;
    public float flashlightSlowDown = 0.65f;

    // Animationen
    public Animator playerAnimController;

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

    private IEnumerator RunRecoveryCooldown()
    {
        runRecovery = true;
        Color col = ausdauerAnzeige.color;
        ausdauerAnzeige.color = Color.red;
        yield return new WaitForSeconds(runRecoveryCooldown);
        ausdauerAnzeige.color = col;
        runRecovery = false;
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
        if(flashlight.gameObject.activeSelf) //slow walking if the Flashlight is currently active
        {
            currentMoveSpeed = Mathf.Lerp(moveSpeed*flashlightSlowDown, runningSpeed, speedChangeTime / speedChangeDuration);
            currentAusdauer = Mathf.Clamp(currentAusdauer + Time.deltaTime * maxAusdauerInSek / regenerationsZeit, 0, maxAusdauerInSek);
            speedChangeTime = Mathf.Clamp01(speedChangeTime - Time.deltaTime);
        }
        else if (isMoving() && Input.GetKey(KeyCode.LeftShift) && !runRecovery && !flashlight.gameObject.activeSelf)   //running
        {
            currentMoveSpeed = Mathf.Lerp(moveSpeed, runningSpeed, speedChangeTime / speedChangeDuration);
            currentAusdauer -= Time.deltaTime;
            speedChangeTime = Mathf.Clamp01(speedChangeTime + Time.deltaTime);

            if(currentAusdauer <= 0)
            {
                StartCoroutine(RunRecoveryCooldown());
            }
        }
        else                                                                        //Recovery for running
        {
            currentMoveSpeed = Mathf.Lerp(moveSpeed, runningSpeed, speedChangeTime / speedChangeDuration);
            currentAusdauer = Mathf.Clamp(currentAusdauer + Time.deltaTime * maxAusdauerInSek / regenerationsZeit, 0, maxAusdauerInSek);
            speedChangeTime = Mathf.Clamp01(speedChangeTime - Time.deltaTime);
        }
        ausdauerAnzeige.fillAmount = currentAusdauer/maxAusdauerInSek;
        
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * currentMoveSpeed; //Move forward/backfords
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * currentMoveSpeed; //Move sidewards
        playerAnimController.SetBool("WalkBool", isMoving());

        MouseLook();
    }
}
