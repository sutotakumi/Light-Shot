using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] AudioClip bgm; //各シーンのBGM
    [SerializeField] AudioSource bgmSource;
    private bool bgmFlag;

    void Start()
    {
        bgmFlag = false;
    }
    
    void Update()
    {
        LoopBGM();
    }

    void LoopBGM()
    {
        if (!bgmFlag)
        {
            bgmSource.PlayOneShot(bgm);
            bgmFlag = true;
        }

        //BGMが流れ終わるとfalseにする
        if (!bgmSource.isPlaying)
        {
            bgmFlag = false;
        }
    }
}
