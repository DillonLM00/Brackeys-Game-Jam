using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public GameObject WinText;
    public float waitForSeconds = 10f;

    private IEnumerator Win()
    {
        WinText.SetActive(true);
        yield return new WaitForSeconds(waitForSeconds);
        WinText.transform.GetChild(0).gameObject.SetActive(true);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Win());
        }
    }
}
