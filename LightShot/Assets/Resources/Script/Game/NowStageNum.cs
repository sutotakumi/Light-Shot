using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NowStageNum : MonoBehaviour
{
    //現在のステージをintに変換
    void Start()
    {
        string stageNum = SceneManager.GetActiveScene().name.Replace("Stage", "");
        Next_or_Retry.stageNumInfo = int.Parse(stageNum);
    }
}
