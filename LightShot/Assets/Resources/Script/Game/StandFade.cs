using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StandFade : MonoBehaviour
{
    [HideInInspector] public float fadeSpeed = 1.5f;
    [HideInInspector] public float red, green, blue, alpha;
    [HideInInspector] public bool isFadeIn = false;
    [HideInInspector] public bool isFadeOut = false;
    [SerializeField] public Image[] fadeImage;

    protected virtual void Start()
    {
        //色
        for (int i = 0; i < fadeImage.Length; i++)
        {
            red = fadeImage[0].color.r;
            green = fadeImage[0].color.g;
            blue = fadeImage[0].color.b;
            alpha = fadeImage[0].color.a;
        }
    }

    protected virtual void Update()
    {

    }

    //フェードのイメージの色を変更
    protected virtual void SetAlpha()
    {
        fadeImage[0].color = new Color(red, green, blue, alpha);
    }
}
