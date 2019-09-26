using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] StandFadeIn sFI;

    //起動
    void Start()
    {
        sFI.isFadeIn = true;
    }
}
