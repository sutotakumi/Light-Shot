using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Player : MonoBehaviour
{
    [HideInInspector] public float speed = 50.0f;//移動速度
    [HideInInspector] public Rigidbody rb;
    public LayerMask groundLayers;//接地判定
    public float jumpforce = 3.5f;//ジャンプの高さ
    public Vector3 movement;
    [HideInInspector] public CapsuleCollider col;
    [SerializeField] GameStatus gS;

    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioSource jumpSource;

    [HideInInspector] public bool isGround;

    [SerializeField] public float jumpTimerCount;
    [SerializeField] public float jumpTime;
    [HideInInspector] public bool isJumping;

    [SerializeField] public Transform eye;

    public static bool seFlag = false;

    private List<ContactPoint> contacts = new List<ContactPoint>();

    [HideInInspector] public float moveHz;
    [HideInInspector] public float moveVt;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        contacts.Clear();
    }

    void Update()
    {
        if (!seFlag)
        {
            //移動やジャンプの処理
            if (!Input.GetKey("joystick button 4"))
            {
                moveHz = Input.GetAxis("L_Stick_H");
                moveVt = Input.GetAxis("L_Stick_V");
            }
            else
            {
                moveHz = 0f;
                moveVt = 0f;
            }

            Vector3 forwardy = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);

            if (Input.GetKey("joystick button 5") && isGround)
            {
                movement = (forwardy.normalized *
                   moveVt + Camera.main.transform.right * moveHz) * speed * 2f;
            }
            else
            {
                movement = (forwardy.normalized *
                     moveVt + Camera.main.transform.right * moveHz) * speed;
            }

            //壁にぶつかった時、下に落ちる
            foreach (var hit in contacts)
            {
                if (Mathf.Abs(hit.normal.y) < 0.1f)
                {
                    if (Vector3.Dot(movement, new Vector3(hit.normal.x, 0f, hit.normal.z).normalized) < 0)
                        movement = Vector3.ProjectOnPlane(movement, new Vector3(hit.normal.x, 0, hit.normal.z).normalized);
                }
            }

            Jump(movement.x, movement.z);

            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
            if (moveHz != 0f || moveVt != 0)
                eye.forward = movement;
        }
    }

    void Jump(float movementX, float movementZ)
    {
        isGround = Physics.CheckCapsule(
                col.bounds.center,
                new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
                col.radius * 0.9f, groundLayers);

        if (isGround && Input.GetKeyDown("joystick button 0") && !Input.GetKey("joystick button 4"))
        {
            isJumping = true;
            rb.velocity = new Vector3(movementX, jumpforce, movementZ);
            jumpTimerCount = jumpTime;
            jumpSource.PlayOneShot(jump);
        }

        if (Input.GetKey("joystick button 0") && isJumping)
        {
            if (jumpTimerCount > 0)
            {
                rb.velocity = new Vector3(movementX, jumpforce, movementZ);
                jumpTimerCount -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp("joystick button 0"))
        {
            isJumping = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //中間地点をリスポーン地点に変更
        if (col.transform.tag == "MiddlePoint")
        {
            Debug.Log("2");
            gS.returnPoint = new Vector3(gS.middlePoint.transform.position.x,
                                         gS.middlePoint.transform.position.y + 5.0f,
                                         gS.middlePoint.transform.position.z);

            gS.middlePoint.SetActive(false);
        }

        //ゴール
        if (col.transform.tag == "Goal")
        {
            gS.goal.SetActive(false);
            gS.sceneFlag = true;
            if (StageSelect.stageNum == StageSelect.stage)
            {
                StageSelect.stageNum++;//ステージ選択でStage追加
            }
        }
    }

    //当たっているcolをリストに入れる
    void OnCollisionStay(Collision col)
    {
        contacts.AddRange(col.contacts);
    }
}
