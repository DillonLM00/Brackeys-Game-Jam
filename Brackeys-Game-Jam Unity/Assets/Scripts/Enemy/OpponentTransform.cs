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

    private float transformFormsDelay = 1f;     // time to change between 0 and number in seconds

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
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
        transformFormsDelay -= Time.deltaTime;         // you can ad a variable to change the time to transform
        if(isEvil && transformFormsDelay <= 0f)
        {
            TransformIntoCute();
        }
    }

    private void TransformIntoEvilOverTime()
    {
        transformFormsDelay += Time.deltaTime * 0.2f;

        if (transformFormsDelay >= 1f)
        {
            TransformIntoEvil();
        }
    }
}
