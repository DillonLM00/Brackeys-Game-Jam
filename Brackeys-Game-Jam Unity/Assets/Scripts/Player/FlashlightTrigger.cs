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

    //private void OnTriggerExit(Collider other)
    //{
    //    OpponentTransform monster = other.transform.parent.GetComponent<OpponentTransform>();

    //    if (other.gameObject.tag == "Monster" && !monster.isEvil)
    //    {
    //       monster.TransformIntoEvil();
    //       Debug.Log("Transform to evil");
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Monster")
    //    {
    //        OpponentTransform monster = other.transform.parent.GetComponent<OpponentTransform>();

    //        if (flashlight.activeSelf && monster.isEvil)
    //        {
    //            monster.TransformIntoCute();
    //            Debug.Log("Transform to cute");
    //        }
    //        else if (!flashlight.activeSelf && !monster.isEvil)
    //        {
    //            monster.TransformIntoEvil();
    //            Debug.Log("Transform to evil2");
    //        }
    //    }
    //}

    // Trasform over Time solution
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            OpponentTransform monster = other.transform.parent.GetComponent<OpponentTransform>();

            if (flashlight.activeSelf)
            {
                monster.TransformIntoCuteOverTime();
                Debug.Log("Transform to cute");
            }
        }
    }
}
