using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class OpponentController : MonoBehaviour
{
    private GameObject player;
    private FirstPersonController fpc;

    private NavMeshAgent agent;
    private Animator animController;

    public float runningSpeed = 5f;
    public float walkingSpeed = 3.5f;
    public float visionRange = 20f;
    public float blickWinkel = 100f;
    public float earRange = 30f;
    public float noiseTolerance = 5f;
    public float noiseMultiplier = 5f;
    public float lightAttraction = 5f;
    [Range(0, 100)]
    public int seeFoolishness = 5;
    [Range(0, 100)]
    public int listenFoolishness = 5;
    [Range(0, 100)]
    public int lightFoolishness = 5;

    private Vector3 lastKnownPlayerPos;
    private Vector3 predictedPlayerPos;
    private Vector3 lastPointOfView;
    private bool predicting = false;
    public float recognizeTime = 0.3f;
    public float recognizeDistance = 5f; //if player recently was in sight, op follows recognizeDistance * 1 Meter
    private bool iRemember = false;

    private bool patrouille = true;
    private Transform patrouillePoint;
    private Transform patrouilleStartPos;
    private bool patrouilleCycle = true;
    public float patrouilleWaitTime = 2f;
    private float notMoving = 0f;

    public LayerMask ignoreTheseColliders;

    private void Awake()
    {
        fpc = FindObjectOfType<FirstPersonController>();
        player = fpc.gameObject;
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        predictedPlayerPos = transform.position;
        patrouilleStartPos = transform.parent.GetChild(0);

        patrouillePoint = transform.parent.GetChild(0).GetChild(0);
    }

    public bool getPatrouilleCycle()
    {
        return patrouilleCycle;
    }

    public void setPatrouilleCycle(bool pC)
    {
        patrouilleCycle = pC;
    }

    private bool foolish(int foolishness)
    {
        return (Random.Range(0, 100) < foolishness);
    }

    private bool playerOnSight()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up, player.transform.position - transform.position, out hit, visionRange, ~ignoreTheseColliders)) //ray in 1meter heigt
        {
            if (hit.transform.gameObject == player)
            {
                Vector3 richtungZumZiel = player.transform.position - transform.position;
                if (Vector3.Angle(transform.forward, richtungZumZiel) <= blickWinkel / 2f)
                {
                        return !foolish(seeFoolishness);
                }
            }
        }

        return false;
    }

    private bool noticingPlayer()
    {
        //how much noise does the player make based on distance and velocity
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= earRange)
        {
            float noise = Mathf.Pow(player.GetComponent<FirstPersonController>().getWalkingSpeed() / distance, 2) * noiseMultiplier;

            if (noise > noiseTolerance && !foolish(listenFoolishness))
            {
                    return true;
            }
        }

        //opponent also gets attracted by light
        if (fpc.flashlightLight.activeSelf)
        {
            Light light = fpc.flashlightLight.GetComponent<Light>();
            float lightDistance = Vector3.Distance(transform.position, light.gameObject.transform.position);

            Vector3 lightDirection = light.gameObject.transform.forward;

            Color col = light.color;
            float colorIntensity = (col.r + col.g + col.b) / 3f;

            if(light.type == LightType.Spot) //this is where direction matters
            {
               if (Vector3.Angle(lightDirection, transform.position - player.transform.position) > 60f) //opponent can see light
               {
                    return false;
               }
            }

            float lightStrength = 1 / (lightDistance * lightDistance) * light.intensity; //inverse-square law of light (fall off)
            lightStrength = lightStrength * 10f * colorIntensity; //factor for normalizing

            return lightStrength >= lightAttraction && !foolish(lightFoolishness);
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
        if (!agent.hasPath)
        {
            animController.SetInteger("WalkInt", 0);
            notMoving += Time.deltaTime;

            if (notMoving >= patrouilleWaitTime)
            {
                patrouille = true;
                notMoving = 0f;
            }
        }
        else
        {
            notMoving = 0f;
            animController.SetInteger("WalkInt", 1);
        }

        DebugVision();

        if (playerOnSight())    //hunting behaviour
        {
            patrouille = false;

            if (!predicting)
                StartCoroutine(lastPos(player.transform.position));

            predictedPlayerPos = player.transform.position;
            agent.speed = runningSpeed;
            animController.SetInteger("WalkInt", 2);

            agent.destination = predictedPlayerPos;
        }
        else if(iRemember)
        {
            iRemember = false;
            lastPointOfView = predictedPlayerPos;
            predictedPlayerPos = predictedPlayerPos + (predictedPlayerPos - lastKnownPlayerPos).normalized * recognizeDistance; //laufe in die Richtung, in die der Player lief
            predicting = false;
            agent.speed = runningSpeed;
            animController.SetInteger("WalkInt", 2);

            agent.destination = predictedPlayerPos;

            Debug.DrawLine(transform.position, predictedPlayerPos, Color.green); //calculated prediction way
            Debug.DrawLine(lastKnownPlayerPos, lastPointOfView, Color.blue);     //the players running direction
        }
        else if(noticingPlayer())    //noticing behaviour
        {
            if (agent.velocity == Vector3.zero || patrouille) //only if the current target is reached
            {
                predictedPlayerPos = transform.position + (player.transform.position - transform.position).normalized * recognizeDistance; //the direction where the noise/light came from
                agent.speed = walkingSpeed;
                animController.SetInteger("WalkInt", 1);

                agent.destination = predictedPlayerPos;
            }
        }
        else if(patrouille)        //Patrouille
        {
            agent.speed = walkingSpeed;
            animController.SetInteger("WalkInt", 1);
            Debug.Log(patrouilleCycle);
            if (patrouilleCycle == true)
            {
                agent.destination = patrouilleStartPos.position;
                patrouilleCycle = false;
            }
            else
            {
                agent.destination = patrouillePoint.position;
                patrouilleCycle = true;
            }

            patrouille = false;
        }
    }
}
