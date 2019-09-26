using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] StandFadeOut sFC;
    [SerializeField] AudioClip decision;
    [SerializeField] AudioSource decisionSource;
    bool decisionFlag;

    void Start()
    {
        decisionFlag = false;
    }

    //起動
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            if (!decisionFlag)
            {
                decisionSource.PlayOneShot(decision);
                decisionFlag = true;
            }
            sFC.isFadeOut = true;
        }
    }
}
