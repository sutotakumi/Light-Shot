using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    [HideInInspector] public int selectCount;

    [SerializeField] public GameObject[] selects;
    [SerializeField] public GameObject cursor;//カーソル
    [SerializeField] public string[] sceneSelect;//遷移先

    private bool selectFlag = false;

    [SerializeField] AudioClip choice;//カーソルが移動する音
    [SerializeField] AudioSource choiceSorce;

    private bool leftFlag = false;
    private bool rightFlag = false;

    void Update()
    {
        SelectScene();
    }

    public void SelectScene()
    {
        Command();
        for (int i = 0; i < selects.Length; i++)
        {
            //selectCountの数値と同じ要素番号が同じだとカーソルがその位置に移動する
            if (selectCount == i && selects[i] != null)
            {
                cursor.transform.position = selects[i].transform.position;
            }
        }
    }

    //selectCountを増減させる
    void Command()
    {

        float selectH = Input.GetAxis("L_Stick_H");

        if (selectH < 0 && !leftFlag)
        {
            choiceSorce.PlayOneShot(choice);
            selectCount--;
            if (selectCount < 0)
            {
                selectCount = 0;
            }
        }
        leftFlag = selectH < 0;


        if (selectH > 0 && !rightFlag)
        {
            if (!selectFlag)
            {
                choiceSorce.PlayOneShot(choice);
                selectFlag = true;
            }
            if (selects[selectCount + 1].activeSelf)//表示している場所だけにカーソルが移動出来るようにする
            {
                selectCount++;
                if (selectCount > selects.Length - 1)
                {
                    selectCount = selects.Length - 1;
                }
            }
        }
        rightFlag = selectH > 0;

        if (selectH <= 0)
            selectFlag = false;
    }
}
