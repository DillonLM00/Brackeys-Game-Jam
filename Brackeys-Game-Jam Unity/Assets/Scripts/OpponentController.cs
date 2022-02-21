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
    private bool predicting = false;
    public float recognizeTime = 0.4f;
    private bool iRemember = true;

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
        if (Vector3.Distance(transform.position, player.transform.position) <= visionRange)
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
        Debug.Log("predicting");
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
                predictedPlayerPos = predictedPlayerPos + lastKnownPlayerPos; //laufe in die Richtung, in die der Player lief
                predicting = false;
            }
            Debug.DrawLine(predictedPlayerPos, lastKnownPlayerPos, Color.blue);
        }

        agent.destination = predictedPlayerPos;
    }
}
