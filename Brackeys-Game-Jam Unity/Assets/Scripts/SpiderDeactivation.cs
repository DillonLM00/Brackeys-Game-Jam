using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDeactivation : MonoBehaviour
{
    public List<OpponentTransform> blackList;
    public GameObject evilReplacer;
    public GameObject harmlessReplacer;
    private GameObject evil;
    private GameObject harmless;

    private void Start()
    {
        evil = blackList[0].evilVersion;
        harmless = blackList[0].harmlessVersion;

        if(!SpiderInformation.spidersActivated)
        {
            swapToReplacer();
        }
    }

    private IEnumerator fixBug(bool cycle)
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < blackList.Count; i++)
        {
            blackList[i].transform.GetComponentInChildren<OpponentController>().setPatrouilleCycle(cycle);
        }
    }

    public void swapToReplacer()
    {
        for (int i = 0; i < blackList.Count; i++)
        {
            Debug.Log("Spidy");
            blackList[i].evilVersion = evilReplacer;
            blackList[i].harmlessVersion = harmlessReplacer;

            Transform pos = blackList[i].transform.GetChild(1).transform;
            Destroy(blackList[i].transform.GetChild(1).gameObject);

            if (blackList[i].isEvil)
            {
                Instantiate(evilReplacer, pos.position, pos.rotation, blackList[i].transform);
                StartCoroutine(fixBug(!blackList[i].transform.GetChild(1).gameObject.GetComponent<OpponentController>().getPatrouilleCycle()));
            }
            else
            {
                Instantiate(harmlessReplacer, pos.position, pos.rotation, blackList[i].transform);
            }
        }
    }

    public void swapToNormal()
    {
        for (int i = 0; i < blackList.Count; i++)
        {
            Debug.Log("No Spidy");
            blackList[i].evilVersion = evil;
            blackList[i].harmlessVersion = harmless;

            Transform pos = blackList[i].transform.GetChild(1).transform;
            Destroy(blackList[i].transform.GetChild(1).gameObject);

            

            if (blackList[i].isEvil)
            {
                Instantiate(evil, pos.position, pos.rotation, blackList[i].transform);
                StartCoroutine(fixBug(!blackList[i].transform.GetChild(1).gameObject.GetComponent<OpponentController>().getPatrouilleCycle()));
            }
            else
            {
                Instantiate(harmless, pos.position, pos.rotation, blackList[i].transform);
            }
        }
    }
}
