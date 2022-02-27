using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    
    private GameObject LoseText;
    private float waitForSeconds = 10f;

    public IEnumerator Lose()
    {
        LoseText.SetActive(true);
        yield return new WaitForSeconds(waitForSeconds);
        LoseText.transform.GetChild(0).gameObject.SetActive(true);
    }
}
