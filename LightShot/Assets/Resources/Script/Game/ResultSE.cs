using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSE : MonoBehaviour
{
    [SerializeField] AudioClip result;//クリア、ゲームオーバー画面のSE
    [SerializeField] AudioSource resultSource;
    void Start()
    {
        resultSource.PlayOneShot(result);
    }
}
