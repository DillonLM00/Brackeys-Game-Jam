using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckboxContinuity : MonoBehaviour
{
    public GameObject checkboxOn;
    public GameObject checkboxOff;

    private void Start()
    {
        if (SpiderInformation.spidersActivated)
        {
            checkboxOn.SetActive(true);
            checkboxOff.SetActive(false);
        }
        else
        {
            checkboxOn.SetActive(false);
            checkboxOff.SetActive(true);
        }
    }
}
