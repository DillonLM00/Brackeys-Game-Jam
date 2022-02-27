using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    
    public GameObject LoseText;
    private float waitForSeconds = 4f;
    private bool firstTime = true;

    private Curortoggle cursortoggle;

    private void Start()
    {
        cursortoggle = GetComponent<Curortoggle>();
    }
    public void lost()
    {
        if (firstTime)
        { 
            StartCoroutine(Lose());
            firstTime = false;
        }
    }

    public IEnumerator Lose()
    {
        cursortoggle.activateCursor();
        LoseText.SetActive(true);
        yield return new WaitForSeconds(waitForSeconds);
        LoseText.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        firstTime = true;
        LoseText.SetActive(false);
    }
}
