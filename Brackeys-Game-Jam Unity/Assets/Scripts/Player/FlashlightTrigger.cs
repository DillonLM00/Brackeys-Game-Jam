using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightTrigger : MonoBehaviour
{
    public GameObject flashlight;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Monster" && flashlight.activeSelf)
    //    {
    //        other.GetComponent<OpponentTransform>().TransformIntoCute();
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<OpponentTransform>().TransformIntoEvil();
        }

        Debug.Log("Transform to evil");
    }

    private void OnTriggerStay(Collider other)
    {
        OpponentTransform monster = other.gameObject.GetComponent<OpponentTransform>();


        if (other.gameObject.tag == "Monster")
        {
            if (flashlight.activeSelf && monster.isEvil)
            {
                monster.TransformIntoCute();
            }
            else if (!flashlight.activeSelf && !monster.isEvil)
            {
                monster.TransformIntoEvil();
            }
        }
    }
}
