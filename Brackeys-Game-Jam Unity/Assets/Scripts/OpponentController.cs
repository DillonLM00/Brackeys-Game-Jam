using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
    private GameObject player;
    public float visionRange = 20f;
    public float blickWinkel = 100f;
    private NavMeshAgent agent;
    public Vector3 lastKnownPlayerPos;
    public Vector3 predictedPlayerPos;
    private Vector3 lastPointOfView;
    private bool predicting = false;
    public float recognizeTime = 0.3f;
    public float recognizeDistance = 5f;
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
                    /*else
                    {
                         transform.LookAt(player.transform);
                    }*/   
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

        if (playerOnSight())
        {
            if(!predicting)
                StartCoroutine(lastPos(player.transform.position));

            predictedPlayerPos = player.transform.position;
        }
        else
        {
            if (iRemember)
            {
                iRemember = false;
                lastPointOfView = predictedPlayerPos;
                predictedPlayerPos = predictedPlayerPos + (predictedPlayerPos - lastKnownPlayerPos).normalized * recognizeDistance; //laufe in die Richtung, in die der Player lief //von A zu B B-A
                predicting = false;
            }

            Debug.DrawLine(transform.position, predictedPlayerPos, Color.green); //calculated prediction way
            Debug.DrawLine(lastKnownPlayerPos, lastPointOfView, Color.blue);     //the players running direction
        }

        agent.destination = predictedPlayerPos;
    }
}
