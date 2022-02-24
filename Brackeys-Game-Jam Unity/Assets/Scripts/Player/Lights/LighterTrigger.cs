using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterTrigger : MonoBehaviour
{
    public Light lighter;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //OpponentTransform monster = other.transform.parent.GetComponent<OpponentTransform>();

            if (lighter.gameObject.activeSelf)
            {
                other.transform.parent.GetComponent<OpponentTransform>().TransformIntoCuteOverTime();
            }
        }
    }
}
