using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveScaffold : MonoBehaviour
{
    [SerializeField] float speed;//移動スピード
    private int pointIndex = 0;//最初の位置
    [SerializeField] Vector3[] pointDate;//床が方向を変える点
    private Vector3 defauitPos;//基準点
    private Rigidbody rb;
    private FixedJoint fj;
    [SerializeField] private Switch @switch;
    private Coroutine coroutine;

    void Start()
    {
        defauitPos = transform.position;
        transform.position = pointDate[pointIndex];
        rb = GetComponent<Rigidbody>();
        fj = GetComponent<FixedJoint>();
        fj.autoConfigureConnectedAnchor = false;
    }

    void FixedUpdate()
    {
        if (!@switch.stand && coroutine == null)
        {
            coroutine = StartCoroutine(enumerator());
        }
        rb.WakeUp();
    }

    //移動処理
    IEnumerator enumerator()
    {

        while (true)
        {
            foreach (Vector3 point in pointDate)
            {
               
                DOTween.To(
                    () => fj.connectedAnchor,
                    pos => fj.connectedAnchor = pos,
                    point,
                    (fj.connectedAnchor - point).magnitude
                    );
                yield return new WaitForSeconds((fj.connectedAnchor - point).magnitude);
            }
        }
    }
}
