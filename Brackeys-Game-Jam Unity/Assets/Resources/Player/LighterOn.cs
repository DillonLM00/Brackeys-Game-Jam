using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterOn : MonoBehaviour
{
    public GameObject sparks;
    public GameObject flame;
    public Animator playerAnimator;
    

    private int random;

    private void Start() {
        random = Random.Range(1,4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AktivateSparks()
    {

    }
}
