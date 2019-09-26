using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    public Select getSelect;
    private AsyncOperation async;
    public GameObject loadingUI; //ロード中に表示するUI
    public Slider slider;//ロード状況を表示

    public void LoadSceneNext()
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }

    //遷移先の情報を読み込み終わったら遷移させる
    IEnumerator LoadScene()
    {
        for (int i = 0; i < getSelect.selects.Length; i++)
        {
            if (getSelect.selectCount == i)
            {
                async = SceneManager.LoadSceneAsync(getSelect.sceneSelect[i]);
            }
        }

        while (!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }
    }
}
