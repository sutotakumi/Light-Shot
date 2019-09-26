using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMoveBlock : MonoBehaviour
{
    private bool scaleFlag;
    [SerializeField] private float max_Scale;//伸びる最大値
    [SerializeField] private float min_Scale;//縮む最小値
    //private float minSpeed = 1.0f;
    //private float maxSpeed = 1.5f;
    //private float randomSpeed;
    [SerializeField] private Switch @switch;

    void Start()
    {
        //0.01よりも大きければそこが最大値で0.01が最小値
        if (transform.localScale.x > 0.01f)
        {
            max_Scale = transform.localScale.x;
            scaleFlag = true;
        }
        //0.01よりも小さいならそこが最小値で1が最大値
        else if (transform.localScale.x <= 0.01f)
        {
            min_Scale = transform.localScale.x;
            scaleFlag = false;
        }
        //randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (!@switch.stand)
            ScaleMove();
    }

    void ScaleMove()
    {
        //縮む
        if (transform.localScale.x > min_Scale && scaleFlag)
        {
            transform.localScale -= new Vector3(0.5f, 0f, 0f) * Time.deltaTime /** randomSpeed*/;
            if (transform.localScale.x <= min_Scale)
            {
                transform.localScale = new Vector3(min_Scale, 1.5f, 1.0f);
                scaleFlag = false;
            }
        }

        //伸びる
        if (transform.localScale.x <= max_Scale && !scaleFlag)
        {
            transform.localScale += new Vector3(0.5f, 0f, 0f) * Time.deltaTime /** randomSpeed*/;
            if (transform.localScale.x > max_Scale)
            {
                transform.localScale = new Vector3(max_Scale, 1.5f, 1.0f);
                scaleFlag = true;
            }
        }
    }
}
