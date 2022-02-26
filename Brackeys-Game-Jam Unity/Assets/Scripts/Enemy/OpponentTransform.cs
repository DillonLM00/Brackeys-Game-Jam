using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentTransform : MonoBehaviour
{
    public GameObject harmlessVersion;
    public GameObject evilVersion;
    public bool isEvil = true;
    private GameObject child;
    private Transform childPos;

    private float transformFormsDelay = 0.25f;         // time to change from Evil to cute;
    private float CuteToEvilTimeMultiplier = 0.125f;  // how long it takes to change back to evil: 0.5 -> twice the time

    private void Start()
    {
        child = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (!isEvil)        
        {
            TransformIntoEvilOverTime();
        }
    }

    // The flashlights light has a trigger collider which interacts with the opponents and transforms them
    public void TransformIntoCute()
    {
        childPos = child.transform;
        Destroy(child);
        child = Instantiate(harmlessVersion, childPos.position, childPos.rotation, gameObject.transform);
        isEvil = false;
    }

    public void TransformIntoEvil()
    {
        childPos = child.transform;
        Destroy(child);
        child = Instantiate(evilVersion, childPos.position, childPos.rotation, gameObject.transform);
        isEvil = true;
    }

    public void TransformIntoCuteOverTime()
    {
        transformFormsDelay -= Time.deltaTime;         
        if(transformFormsDelay <= 0f)
        {
            transformFormsDelay = 0f;           // caps the length of transformation, remove to increase time corresponding to light length
            if (isEvil)
            {
                TransformIntoCute();
            }
        }
    }

    private void TransformIntoEvilOverTime()
    {
        transformFormsDelay += Time.deltaTime * CuteToEvilTimeMultiplier;

        if (transformFormsDelay >= 1f)
        {
            TransformIntoEvil();
        }
    }
}
