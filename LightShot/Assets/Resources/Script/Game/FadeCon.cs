using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeCon : MonoBehaviour
{
    //フェード用のcanvasとimage
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    //フェード用imageの透明度
    private static float alpha = 0.0f;

    //フェードイン、アウトのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //フェードしたい時間
    private static float fadeTime = 7.5f;

    //遷移先のシーン番号
    private static int nextScene = 5;

    //フェード用のCanvasとImage生成
    void Init()
    {
        //フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeCon>();

        //最前面に設定
        fadeCanvas.sortingOrder = 100;

        //フェード用のImage生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.sizeDelta = new Vector2(1930, 1090);
    }

    //フェードアウト開始
    public void FadeOut()
    {
        if (fadeImage == null)
            Init();
        fadeImage.color = Color.black;
        isFadeOut = true;
    }

    //フェードイン開始
    public void FadeIn()
    {
        if (fadeImage == null)
            Init();
        fadeImage.color = Color.clear;
        fadeCanvas.enabled = true;
        isFadeIn = true;

    }

    void Update()
    {
        //フラグが真なら毎フレームフェードイン、アウト
        if (isFadeIn)
        {
            //経過時間から透明度を計算
            alpha -= Time.deltaTime / fadeTime;

            //フェードイン終了判定
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }

            //フェード用のImageの透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {
            //経過時間から透明度計算
            alpha += Time.deltaTime / fadeTime;

            //フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;
            }

            //フェード用のImageの透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}
