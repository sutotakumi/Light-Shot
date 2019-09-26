using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float time;

    [SerializeField] AudioClip open; //開ける音
    [SerializeField] AudioSource openSource;

    [SerializeField] AudioClip close; //閉める音 
    [SerializeField] AudioSource closeSource;

    void Update()
    {
        //閉じる
        if (transform.parent.rotation != Quaternion.Euler(0f, 0f, 0f))
        {
            time++;
            if (time >= 180f)
            {
                closeSource.PlayOneShot(close);
                time = 0;
                transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //開ける
        if (col.gameObject.tag == "Player" &&
            transform.parent.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            openSource.PlayOneShot(open);
            transform.parent.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }
}
