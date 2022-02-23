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
        OpponentTransform monster = other.transform.parent.GetComponent<OpponentTransform>();

        if (other.gameObject.tag == "Monster" && !monster.isEvil)
        {
           monster.TransformIntoEvil(monster.transform.GetChild(0).transform);
           Debug.Log("Transform to evil");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            OpponentTransform monster = other.transform.parent.GetComponent<OpponentTransform>();

            if (flashlight.activeSelf && monster.isEvil)
            {
                monster.TransformIntoCute(monster.transform.GetChild(0).transform);
                Debug.Log("Transform to cute");
            }
            else if (!flashlight.activeSelf && !monster.isEvil)
            {
                monster.TransformIntoEvil(monster.transform.GetChild(0).transform);
                Debug.Log("Transform to evil2");
            }
        }
    }
}
