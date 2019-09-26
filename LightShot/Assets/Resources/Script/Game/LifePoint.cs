using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoint : MonoBehaviour
{
    [SerializeField] GameStatus status;
    [HideInInspector] Canvas uiRectPos;
    [HideInInspector] public Vector2 lifeTextPos; //残機の表記場所
    [HideInInspector] public Vector2 lifeTextCenter; //残機が減るとき、フェード中残機の表示を中心に持ってくる
    public Text lifeText;//life表示

    void Start()
    {
        uiRectPos = GetComponentInParent<Canvas>();
        lifeTextPos = lifeText.rectTransform.position;
        lifeTextCenter = uiRectPos.pixelRect.center;
        lifeText.text = "" + status.life;
    }

    public void LifeCenter()
    {
        lifeText.rectTransform.position = lifeTextCenter;
    }

    //残りlifeの表示
    public void Life()
    {
        lifeText.text = "" + status.life;
    }
}
