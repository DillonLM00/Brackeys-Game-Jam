using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLerp : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float camSpeed;
    private float time;

    private void Update()
    {
        time += Time.deltaTime * camSpeed;
        this.transform.position = Vector3.Lerp(startPos.position, endPos.position, time);
    }
}
