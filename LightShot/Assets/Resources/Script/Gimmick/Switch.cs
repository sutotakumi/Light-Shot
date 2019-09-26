using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [HideInInspector] public bool stand; //起動

    //起動音
    [SerializeField] AudioClip switchStand;
    [SerializeField] AudioSource switchStandSource;
    private bool seFlag;

    protected virtual void Start()
    {
        stand = true;
        seFlag = true;
    }

    protected virtual void Update()
    {
        if (!stand && seFlag)
            SwitchSE();
    }

    void SwitchSE()
    {
        switchStandSource.PlayOneShot(switchStand);
        seFlag = false;

    }
}
