using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandFadeIn : StandFade
{
    //UI関係のFade
    [HideInInspector] public float t1Red, t2Red, t1Green, t2Green, t1Blue, t2Blue, tAlpha;
    [SerializeField] public Text[] fadeText;

    protected override void Start()
    {
        base.Start();
        alpha = 1.0f;
        t1Red = fadeText[0].color.r;
        t1Green = fadeText[0].color.g;
        t1Blue = fadeText[0].color.b;
        t2Blue = t2Green = t2Red = 0;
        tAlpha = 0.0f;
    }

    protected override void Update()
    {
        base.Update();
        if (isFadeIn)
        {
            StartFadeIn();
        }
    }

    //フェードインの処理
    void StartFadeIn()
    {
        for (int i = 0; i < fadeImage.Length; i++)
            fadeImage[i].enabled = true;
        alpha -= Time.deltaTime / fadeSpeed;
        tAlpha += Time.deltaTime / fadeSpeed;
        SetAlpha();
        if (alpha <= 0.0f)
        {
            isFadeIn = false;
        }
    }

    protected override void SetAlpha()
    {
        base.SetAlpha();
        for (int i = 0; i < fadeText.Length; i++)
        {
            if (i <= 1)
                fadeText[i].color = new Color(t1Red, t1Green, t1Blue, tAlpha);
            else if (i >= 2)
                fadeText[i].color = new Color(t2Red, t2Green, t2Blue, tAlpha);
        }
    }
}
