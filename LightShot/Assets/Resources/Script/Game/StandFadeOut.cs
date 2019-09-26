using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StandFadeOut : StandFade
{
    [SerializeField] LoadScreen load;
    //UI関係のFade
    [HideInInspector] public float t1Red, t2Red, t1Green, t2Green, t1Blue, t2Blue, tAlpha;
    [SerializeField] public Text[] fadeText;
    [SerializeField] Select getSelect;

    protected override void Start()
    {
        base.Start();
        alpha = 0.0f;
        t1Red = fadeText[0].color.r;
        t1Green = fadeText[0].color.g;
        t1Blue = fadeText[0].color.b;
        t2Red = t2Green = t2Blue = 0;
        tAlpha = 1.0f;
    }

    protected override void Update()
    {
        base.Update();

        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    //フェードアウトの処理
    void StartFadeOut()
    {
        for (int i = 0; i < fadeImage.Length; i++)
            fadeImage[i].enabled = true;
        alpha += Time.deltaTime / fadeSpeed;
        tAlpha -= Time.deltaTime / fadeSpeed;
        SetAlpha();
        //フェードアウトにより画面が暗くなるとLoad開始
        if (alpha >= 1)
        {
            isFadeOut = false;
            load.LoadSceneNext();
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

            if (fadeText[i].transform.name == "Pause")
                fadeText[i].color = new Color(t1Red, t1Green, t1Blue, tAlpha);

        }

        for (int i = 0; i < fadeImage.Length; i++)
        {
            if (i >= 1)
                fadeImage[1].color = new Color(255, 255, 255, tAlpha);
        }
    }
}
