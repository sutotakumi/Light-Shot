using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStatus : StandFade
{
    public GameObject player;
    private Rigidbody rb;

    public Vector3 returnPoint;//リスポーン地点
    private bool dropDownFade = false;//map落ちた時のフェードアウト
    private bool gameReStartFade = false;//リスポーン地点に戻った時のフェードイン
    public GameObject middlePoint;//中間地点
    public GameObject goal;//中間地点

    public int life;
    [SerializeField] LifePoint lifePoint;
    private bool lifeCountFlag;
    [SerializeField] public string[] sceneSelect; //クリアorゲームオーバー
    public bool sceneFlag; //false...ゲームオーバー ,true...クリア
    private bool clearFadeFlag;

    [SerializeField] Image rMI;
    private float rMIAlpha;
    bool rMIFlag;

    protected override void Start()
    {
        base.Start();
        rMIAlpha = 0f;
        rMIFlag = false;
        alpha = 0.0f;
        middlePoint = GameObject.Find("MiddlePoint");
        goal = GameObject.Find("Goal");
        returnPoint = new Vector3(player.transform.position.x,
                                  player.transform.position.y + 2.5f,
                                  player.transform.position.z);
        lifeCountFlag = false;
        rb = player.GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();

        if (!rMIFlag)
            RMIA();

        DropDown();

        if (gameReStartFade)
        {
            Invoke("ReStart", 1.5f);
        }

        if (!sceneFlag && life == 0 || sceneFlag)
            Invoke("GameResult", 1.5f);
    }

    void DropDown()
    {
        if (player.transform.position.y <= -5f)
        {
            dropDownFade = true;
        }
        if (dropDownFade)
        {
            FadeOut();
        }
    }

    //フェードアウト
    void FadeOut()
    {
        for (int i = 0; i < fadeImage.Length; i++)
            fadeImage[0].enabled = true;
        alpha += Time.deltaTime;
        SetAlpha();
        if (alpha >= 1f)
        {
            if (!sceneFlag)
            {
                lifePoint.LifeCenter();

                dropDownFade = false;
                gameReStartFade = true;

                if (life == 0)
                {
                    sceneFlag = false;
                }
                else
                {
                    if (!lifeCountFlag)
                    {
                        lifeCountFlag = true;
                        life--;
                    }
                    lifePoint.Life();
                }
            }
            else if (clearFadeFlag)
            {
                clearFadeFlag = false;
            }
        }
    }

    //フェードイン
    void ReStart()
    {
        for (int i = 0; i < fadeImage.Length; i++)
            fadeImage[0].enabled = true;
        if (life > 0)
            lifePoint.lifeText.rectTransform.position = lifePoint.lifeTextPos;
        alpha -= Time.deltaTime;
        SetAlpha();
        if (alpha >= 0.9f)
        {
            rb.velocity = Vector3.zero;
            player.transform.position = returnPoint;
        }

        if (alpha <= 0f)
        {
            gameReStartFade = false;
            lifeCountFlag = false;
        }
    }

    //残機アイコンのフェード
    void RMIA()
    {
        rMIAlpha += Time.deltaTime / fadeSpeed;
        rMI.color = new Color(rMI.color.r, rMI.color.g, rMI.color.b, rMIAlpha);
        if (rMIAlpha >= 1f)
            rMIFlag = true;
    }

    //lifeが0になるとゲームオーバー,ゴールに触れたらクリアー
    void GameResult()
    {
        if (!sceneFlag)
            SceneManager.LoadScene(sceneSelect[0]);
        else if (sceneFlag)
        {
            clearFadeFlag = true;
            FadeOut();
            if (!clearFadeFlag)
                SceneManager.LoadScene(sceneSelect[1]);
        }
    }

    protected override void SetAlpha()
    {
        base.SetAlpha();
    }
}
