using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponnentTransform : MonoBehaviour
{
    public GameObject harmlessVersion;
    public GameObject evilVersion;
    private GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // This is untested!!!!
    // The flashlights light has a trigger collider which interacts with the opponents and transforms them
    private void OnTriggerEnter(Collider other)
    {
        Destroy(child);
        child = Instantiate(harmlessVersion, gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(child);
        child = Instantiate(evilVersion, gameObject.transform);
    }
}
