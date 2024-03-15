using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeTextAnimation : MonoBehaviour
{
    public float fadeDuration = 2f;
    private float currentFadeTime;
    private CanvasGroup canvasGroup;
    private bool fadingIn = true;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (fadingIn)
        {
            currentFadeTime += Time.deltaTime;
            canvasGroup.alpha = currentFadeTime / fadeDuration;
            if (currentFadeTime >= fadeDuration)
            {
                fadingIn = false;
                currentFadeTime = 0;
            }
        }
        else
        {
            currentFadeTime += Time.deltaTime;
            canvasGroup.alpha = 1 - currentFadeTime / fadeDuration;
            if (currentFadeTime >= fadeDuration)
            {
                fadingIn = true;
                currentFadeTime = 0;
            }
        }
    }
}
