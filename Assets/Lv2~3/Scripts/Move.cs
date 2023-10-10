using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    bool isStop = false;

    /// <summary>無敵モード</summary>
    [SerializeField] bool m_godMode = false;
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    /// <summary>弾の発射位置</summary>
    [SerializeField] Transform m_muzzle = null;
    /// <summary>一画面の最大段数 (0 = 無制限)</summary>
    [SerializeField, Range(0, 10)] int m_bulletLimit = 0;
    /// <summary>プレイヤーの位置</summary>
    [SerializeField] Vector3 _vector3;

    /// <summary>左の壁</summary>
    [SerializeField] float _leftWall;
    /// <summary>右の壁</summary>
    [SerializeField] float _rightWall;
    /// <summary>移動</summary>
    Vector3 _dir = default;

    //CharacterController controller;
    Vector3 movedir = Vector3.zero;

    int Lane;

    public float speedX;
    public float speedZ;
    public float acceleratorZ;


    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _vector3 = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 自機を移動させる
        //float h = Input.GetAxisRaw("Horizontal");   // 垂直方向の入力を取得する
        //float v = Input.GetAxisRaw("Vertical");     // 水平方向の入力を取得する
        //_dir = new Vector3(h, 0, 0).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
        //_dir = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward;

        //if (!isStop)
        //{
            //_rigidBody.velocity = _dir * m_moveSpeed; //単位ベクトルにスピードをかけて速度ベクトルにして、それを Rigidbody の速度ベクトルとしてセットする
            //_rigidBody.velocity = new Vector3(0, 0, 10f);
        //}
        //else
        //{
            //_rigidBody.velocity = Vector3.zero;
        //}


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Lane > -2f)
            {
                Lane--;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Lane < 2f)
            {
                Lane++;
            }
        }
        //if (Input.GetKeyDown("space"))
        //{
        //    if (controller.isGrounded)
        //    {
        //        movedir.y = 10f;
        //    }
        //}

        movedir.z = Mathf.Clamp(movedir.z + (acceleratorZ * Time.deltaTime), 0, speedZ);

        float ratioX = (Lane * 1.0f - transform.position.x) / 1.0f;
        movedir.x = ratioX * speedX;



        movedir.y -= 20f * Time.deltaTime;

        Vector3 globaldir = transform.TransformDirection(movedir);
        //controller.Move(globaldir * Time.deltaTime);

        //if (controller.isGrounded)
        //{
        //    movedir.y = 0;
        //}
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Goal Ground")
        {
            isStop = true;
        }
    }
}