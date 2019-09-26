using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PCamera : MonoBehaviour
{
    public GameObject target; //プレイヤー
    public Vector3 offset; //カメラ初期位置

    [SerializeField] private float distance; //プレイヤーとの距離
    private const float MaxDistance = 10.0f;
    [SerializeField] private float SPEED; //カメラの回転速度
    [SerializeField] private float polarAngle; // angle with y-axis
    [SerializeField] private float azimuthalAngle; // angle with x-axis
    [SerializeField] private float minPolarAngle; // y軸最小値
    [SerializeField] private float maxPolarAngle; // y軸最大値
    [SerializeField] private float mouseXSensitivity; // X感度
    [SerializeField] private float mouseYSensitivity; // Y感度

    [SerializeField] private List<string> coverLayer;//遮蔽物のリスト
    private int layerMask;//遮蔽物のレイヤーマスク

    [SerializeField] public List<Renderer> rendererHitsList = new List<Renderer>();

    [SerializeField] public Renderer[] rendererHitsPrevs;

    void Start()
    {
        //マウスを固定して見えなくする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //遮蔽物のレイヤーマスクを、レイヤー名から合成
        layerMask = 0;
        foreach (string layerName in coverLayer)
        {
            layerMask |= 1 << LayerMask.NameToLayer(layerName);
        }
    }

    void LateUpdate()
    {
        updateAngle(Input.GetAxis("R_Stick_H"), Input.GetAxis("R_Stick_V")); // x軸y軸の値を受け取りupdateAngleに渡す
        var lookAtPos = target.transform.position + offset;
        updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity * 0.5f;
        azimuthalAngle = Mathf.Repeat(x, 360); // x軸の最大値設定

        y = polarAngle + y * mouseYSensitivity * 0.25f;
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle); // y軸の最大値設定

        lookPlayer();
    }

    // プレイヤーを中心に正しく回転させる
    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }

    //プレイヤーを見えるようにする
    void lookPlayer()
    {
        //被写体からカメラにRayを出す        
        Vector3 differnce = transform.position - target.transform.position;
        Vector3 direction = differnce.normalized;
        Ray ray = new Ray(target.transform.position, direction);

        //遮蔽物を取得
        RaycastHit[] hits = Physics.RaycastAll(ray, differnce.magnitude, layerMask);

        rendererHitsPrevs = rendererHitsList.ToArray();
        rendererHitsList.Clear();
        //遮蔽物カメラが遮蔽物がないところに移動
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == target)
            {
                continue;
            }

            Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                rendererHitsList.Add(renderer);
                renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.75f);
            }
        }

        foreach (Renderer renderer in rendererHitsPrevs.Except<Renderer>(rendererHitsList))
        {
            if (renderer != null)
            {
                renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1.0f);
            }
        }
    }
}
