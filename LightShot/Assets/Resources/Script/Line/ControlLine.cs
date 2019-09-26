using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLine : MonoBehaviour
{
    [SerializeField] AudioClip line; //光の音
    [SerializeField] AudioSource lineSource;

    private bool flag = false;
    private bool flagLog = false;
    private bool f = false;

    void FixedUpdate()
    {
        if (flagLog != flag && !f)
        {
            f = true;
            lineSource.Play();
        }


        flagLog = flag;
    }

    void Update()
    {
        RotateLaunch();

        LineSE();
    }

    // キーで向きを変える
    void RotateLaunch()
    {
        float lineH = Input.GetAxis("L_Stick_H");
        float lineV = Input.GetAxis("L_Stick_V");
        if (Input.GetKey("joystick button 4"))
        {
            if (lineV > 0)
            {
                transform.eulerAngles += new Vector3(0f, 0f, 0.18f);
            }
            if (lineV < 0)
            {
                transform.eulerAngles -= new Vector3(0f, 0f, 0.18f);
            }

            if (lineH > 0)
            {
                transform.eulerAngles += new Vector3(0f, 0.8f, 0f);
            }
            if (lineH < 0)
            {
                transform.eulerAngles -= new Vector3(0f, 0.8f, 0f);
            }
        }
    }

    void LineSE()
    {
        if (Input.GetKey("joystick button 4"))
        {
            flag = !flag;
            //途切れを消す
            if (lineSource.time >= line.length - 0.1f)
            {
                lineSource.time = 0.5f;
            }
        }
        else
        {
            if (flagLog == flag)
            {
                f = false;
                lineSource.Stop();
            }
        }
    }
}
