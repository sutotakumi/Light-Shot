using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_or_Retry : MonoBehaviour
{
    [SerializeField] Select getSelect;
    [HideInInspector] public static int stageNumInfo;//現在のステージ番号
    [HideInInspector] public int nextStage;//次のステージ番号
    [HideInInspector] public int retryStage;//ゲームオーバーしたステージ番号

    void Start()
    {
        nextStage = stageNumInfo + 1;
        retryStage = stageNumInfo;
    }

    void Update()
    {
        N_or_R();
    }

    //Nextを押すと次のステージへ、Retryを押すとリトライ
    void N_or_R()
    {
        for (int i = 0; i < getSelect.selects.Length; i++)
        {
            if (getSelect.selects[1].name == "Next")
            {
                getSelect.sceneSelect[1] = "Stage" + nextStage;
            }
            else if (getSelect.selects[1].name == "Retry")
            {
                getSelect.sceneSelect[1] = "Stage" + retryStage;
            }
        }
    }
}
