using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject selectobj;
    [SerializeField] Select select;
    [SerializeField] StandFadeOut fadeOut;
    [SerializeField] FadeOut fade;
    static public bool flag = false;

    [SerializeField] AudioClip open;
    [SerializeField] AudioSource openSource;

    [SerializeField] AudioClip close;
    [SerializeField] AudioSource closeSource;

    void Start()
    {
        Player.seFlag = false;
        flag = false;
        pause.SetActive(false);
        camera.SetActive(false);
        cursor.SetActive(false);
        selectobj.SetActive(false);
        GetComponent<FadeOut>().enabled = false;
    }

    void Update()
    {
        //ポーズ画面に使うものをtrue
        if (Input.GetKeyDown("joystick button 7") && !flag)
        {
            Player.seFlag = true;
            flag = true;
            pause.SetActive(true);
            camera.SetActive(true);
            cursor.SetActive(true);
            selectobj.SetActive(true);
            GetComponent<FadeOut>().enabled = true;
            openSource.PlayOneShot(open);
        }
        //ポーズ画面に使わないものをfalse
        else if (Input.GetKeyDown("joystick button 7") && flag)
        {
            Player.seFlag = false;
            flag = false;
            pause.SetActive(false);
            camera.SetActive(false);
            cursor.SetActive(false);
            selectobj.SetActive(false);
            GetComponent<FadeOut>().enabled = false;
            
            closeSource.PlayOneShot(close);
        }
        if (fadeOut.alpha >= 0.9)
            cursor.SetActive(false);
    }
}
