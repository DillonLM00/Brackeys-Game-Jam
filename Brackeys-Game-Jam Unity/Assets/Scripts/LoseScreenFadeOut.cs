using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenFadeOut : MonoBehaviour
{
    private Image fadeOut;
    public float fadeSpeed = 5f;
    private float alpha = 0f;
    private FirstPersonController player;

    private void Start()
    {
        //fadeOut = GetComponent<Image>();
        
        player = FindObjectOfType<FirstPersonController>();

       // alpha = 0f;
    }

    //private void Update()
    //{
    //    if (alpha >= 1)
    //        player.setToCheckpoint();

    //    alpha += Time.deltaTime / fadeSpeed;
    //    fadeOut.color = new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, alpha);
    //}

    public void respawnPlayer()
    {
        player.setToCheckpoint();
    }
}
