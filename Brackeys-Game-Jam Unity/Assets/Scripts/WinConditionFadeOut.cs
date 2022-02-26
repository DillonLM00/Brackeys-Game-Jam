using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinConditionFadeOut : MonoBehaviour
{
    private Image fadeOut;
    public float fadeSpeed = 5f;
    private float alpha = 0f;

    private void Start()
    {
        fadeOut = GetComponent<Image>();
    }

    private void Update()
    {
        if (alpha >= 1)
            SceneManager.LoadScene("MainMenu");

        alpha += Time.deltaTime / fadeSpeed;
        fadeOut.color = new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, alpha);
    }
}
