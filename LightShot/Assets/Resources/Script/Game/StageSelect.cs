using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Select getSelect;
    public static int stageNum = 1;//遊べるステージの数
    public static int stage = 1; //クリアしたStageをクリアしたときに新しいStageに行けるようになるのを防ぐ

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Fade")
            {
                int i = 0;
                foreach (Transform child2 in child.transform)
                {
                    if (child2.gameObject.name != "Slider")
                    {
                        i++;
                        if (stageNum < i)//クリアしていないステージを非表示にさせる
                        {
                            child2.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        stage = getSelect.selectCount + 1;
    }
}
