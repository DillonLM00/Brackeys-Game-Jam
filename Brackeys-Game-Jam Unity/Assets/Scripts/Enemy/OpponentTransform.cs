using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentTransform : MonoBehaviour
{
    public GameObject harmlessVersion;
    public GameObject evilVersion;
    public bool isEvil = true;
    private GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // The flashlights light has a trigger collider which interacts with the opponents and transforms them
    public void TransformIntoCute(Transform pos)
    {
        Destroy(child);
        child = Instantiate(harmlessVersion, pos.position, pos.rotation, gameObject.transform);
        isEvil = false;
    }

    public void TransformIntoEvil(Transform pos)
    {
        Destroy(child);
        child = Instantiate(evilVersion, pos.position, pos.rotation, gameObject.transform);
        isEvil = true;
    }
}
