using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent agent;

    public float visionRange = 20f;
    public float blickWinkel = 100f;
    public float earshot = 30f;
    public float noiseTolerance = 5f;
    public float noiseMultiplier = 5f;

    private Vector3 lastKnownPlayerPos;
    private Vector3 predictedPlayerPos;
    private Vector3 lastPointOfView;
    private bool predicting = false;
    public float recognizeTime = 0.3f;
    public float recognizeDistance = 5f; //if player recently was in sight, op follows recognizeDistance * 1 Meter
    private bool iRemember = false;

    public LayerMask ignoreTheseColliders;

    private void Awake()
    {
        player = FindObjectOfType<FirstPersonController>().gameObject;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        predictedPlayerPos = transform.position;
    }

    private bool playerOnSight()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position-transform.position, out hit, visionRange, ~ignoreTheseColliders))
        {
            if(hit.transform.gameObject == player)
            {
                    Vector3 richtungZumZiel = player.transform.position - transform.position;
                    if (Vector3.Angle(transform.forward, richtungZumZiel) <= blickWinkel / 2f)
                    {
                        return true;
                    }
            }
        }

        return false;
    }

    private bool hearingPlayer() //wahrscheinlichkeit, dass opponent den player überhört oder übersieht
    {
        //how much noise does the player make based on distance and velocity
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= earshot)
        {
            float noise = Mathf.Pow(player.GetComponent<FirstPersonController>().getWalkingSpeed() / distance, 2) * noiseMultiplier;
            //Debug.Log(noise);

            if (noise > noiseTolerance)
            {
                return true;
            }
        }
        
        return false;
    }

    private void DebugVision()
    {
        Vector3 richtungWinkel = Quaternion.Euler(0, -blickWinkel / 2, 0) * transform.forward * visionRange;
        Debug.DrawLine(transform.position, transform.position + richtungWinkel, Color.red);
        richtungWinkel = Quaternion.Euler(0, blickWinkel / 2, 0) * transform.forward * visionRange;
        Debug.DrawLine(transform.position, transform.position + richtungWinkel, Color.red);
    }

    private IEnumerator lastPos(Vector3 pos)
    {
        predicting = true;
        lastKnownPlayerPos = pos;
        iRemember = true;
        yield return new WaitForSeconds(recognizeTime);
        predicting = false;
    }

    private void Update()
    {
        DebugVision();

        if (playerOnSight())    //hunting behaviour
        {
            if(!predicting)
                StartCoroutine(lastPos(player.transform.position));

            predictedPlayerPos = player.transform.position;
        }
        else if(iRemember)
        {
            iRemember = false;
            lastPointOfView = predictedPlayerPos;
            predictedPlayerPos = predictedPlayerPos + (predictedPlayerPos - lastKnownPlayerPos).normalized * recognizeDistance; //laufe in die Richtung, in die der Player lief //von A zu B B-A
            predicting = false;

            Debug.DrawLine(transform.position, predictedPlayerPos, Color.green); //calculated prediction way
            Debug.DrawLine(lastKnownPlayerPos, lastPointOfView, Color.blue);     //the players running direction
        }
        else if(hearingPlayer())    //listening behaviour
        {
            transform.LookAt(player.transform); //walk in the direction of the noise
        }
        else         //normal behaviour
        {
            //Patrouillie
        }

        agent.destination = predictedPlayerPos;
    }
}
