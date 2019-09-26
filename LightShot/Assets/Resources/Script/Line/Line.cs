using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Line : MonoBehaviour
{
    [HideInInspector] public List<Vector3> keepPoints; //反射点の保存
    private List<BoxCollider> boxs; //光のcollider
    const int MaxRefCount = 5; //最大反射数
    private ParticleSystem line; //光
    private Vector3 lastPoint; //RayがObjectに当たらないときも光を出すための座標
    const float MaxLength = 50.0f; //光の最大距離

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.tag == "Line")
            {
                line = child.gameObject.GetComponent<ParticleSystem>();
            }
        }
        keepPoints = new List<Vector3>();
        boxs = new List<BoxCollider>();
    }

    void Update()
    {
        keepPoints = new List<Vector3>();
        keepPoints.Add(new Vector3(transform.position.x, transform.position.y, transform.position.z));

        if (Input.GetKey("joystick button 4"))
        {
            //光の向き
            DrawReflect(transform.position + transform.right * 0.75f, transform.right);

            RightCollider();

            DrawRight();
        }
        //ボタンが押されてないので光を出さない
        else
        {
            for (int i = 0; i < boxs.Count; i++)
            {
                boxs[i].gameObject.SetActive(false);
            }
        }
    }

    //光のコライダー
    void RightCollider()
    {
        //コライダーより光の線の数が少なければその差分非表示
        foreach (BoxCollider b in boxs.Skip(keepPoints.Count - 1))
        {
            b.gameObject.SetActive(false);
        }

        //光の線の数よりコライダーが足りなければならその差分生成
        for (int i = 0; i < keepPoints.Count - 1; i++)
        {
            //コライダー生成
            if (boxs.Count <= i)
            {
                GameObject box = new GameObject("Collider");
                boxs.Add(box.AddComponent<BoxCollider>());
                box.gameObject.tag = "Box";
                box.GetComponent<BoxCollider>().isTrigger = true;
                box.transform.parent = transform;
            }

            //コライダーが光の線の数以上あるなら生成せずに同じ数だけ表示
            if (!boxs[i].gameObject.activeSelf)
            {
                boxs[i].gameObject.SetActive(true);
            }
            //光に合わせる
            boxs[i].transform.rotation = Quaternion.FromToRotation(Vector3.left, keepPoints[i] - keepPoints[i + 1]);
            boxs[i].transform.position = Vector3.Lerp(keepPoints[i], keepPoints[i + 1], 0.5f);
            boxs[i].transform.localScale = new Vector3((keepPoints[i] - keepPoints[i + 1]).magnitude + 1, 1f, 1f);
        }
    }

    //光の線
    void DrawRight()
    {
        for (int i = 0; i < keepPoints.Count - 1; i++)
        {
            //Particle同士の間隔
            float range = (keepPoints[i] - keepPoints[i + 1]).magnitude * 10;

            //Particleを並べる
            for (int j = 0; j < range; j++)
            {
                ParticleSystem.EmitParams emit = new ParticleSystem.EmitParams();
                emit.position =
                    line.transform.InverseTransformPoint(
                        Vector3.Lerp(keepPoints[i], keepPoints[i + 1], j / range));
                line.Emit(emit, 1);
            }
        }
    }

    //光の反射
    void DrawReflect(Vector3 pos, Vector3 dir)
    {
        //光の最初の位置
        Vector3 startPos = new Vector3(pos.x, pos.y, pos.z);

        RaycastHit[] hits = Physics.RaycastAll(pos, dir, MaxLength);
        hits = hits.OrderBy(x => x.distance).ToArray();

        if (hits.Length != 0) //Rayが何かに当たった時
        {
            //何かにRayが当たった時に反射できるのか
            foreach (RaycastHit hit in hits)
            {
                //光が反射するものに当たったらその点を最初の位置にしてDrawReflectを再度実行
                if (IsRef(hit) && keepPoints.Count <= MaxRefCount)
                {
                    RefrectRay(hit, ref pos, ref dir);
                    DrawReflect(pos, dir);
                    break;
                }
                //光が反射しないものに当たったらその点を取得
                else if (NoRef(hit))
                {
                    RefrectRay(hit, ref pos, ref dir);
                    break;
                }
                //ギミック起動スイッチに当たると起動させる
                else if (hit.collider.tag == "Switch")
                {
                    hit.collider.GetComponent<Switch>().stand = false;
                    RefrectRay(hit, ref pos, ref dir);
                    break;
                }
            }
        }
        //光がオブジェクトに当たってないとき最大距離まで伸ばす
        else if (hits.Length == 0 || keepPoints.Count <= MaxRefCount)
        {
            //最大距離
            lastPoint = pos + dir * MaxLength;
            keepPoints.Add(lastPoint);
        }
    }

    //反射する
    bool IsRef(RaycastHit hit)
    {
        if (hit.collider == null)
            return false;

        if (hit.collider.tag == "Refrect")
            return true;

        return false;
    }

    //反射しない
    bool NoRef(RaycastHit hit)
    {
        if (hit.collider == null)
            return false;

        if (hit.collider.tag == "NotRefrect")
            return true;

        if (hit.collider.tag == "MScaffold")
            return true;

        if (hit.collider.tag == "ScaleMove")
            return true;

        return false;
    }

    //次点の情報
    void RefrectRay(RaycastHit hit, ref Vector3 pos, ref Vector3 dir)
    {
        dir = Vector3.Reflect(dir, hit.normal);
        pos = hit.point;
        keepPoints.Add(new Vector3(pos.x, pos.y, pos.z));
    }
}
