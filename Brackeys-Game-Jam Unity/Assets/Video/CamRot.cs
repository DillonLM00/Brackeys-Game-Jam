using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour
{
    public float angle = 5f;
    public GameObject point;
    public float camSpeed;

    private void Update()
    {
        angle = Time.deltaTime * camSpeed;
        this.transform.RotateAround(point.transform.position, Vector3.up, angle);
    }
}
