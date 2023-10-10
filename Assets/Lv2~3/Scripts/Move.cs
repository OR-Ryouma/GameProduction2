using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    bool isStop = false;

    /// <summary>���G���[�h</summary>
    [SerializeField] bool m_godMode = false;
    /// <summary>�v���C���[�̈ړ����x</summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary>�e�̃v���n�u</summary>
    [SerializeField] GameObject m_bulletPrefab = null;
    /// <summary>�e�̔��ˈʒu</summary>
    [SerializeField] Transform m_muzzle = null;
    /// <summary>���ʂ̍ő�i�� (0 = ������)</summary>
    [SerializeField, Range(0, 10)] int m_bulletLimit = 0;
    /// <summary>�v���C���[�̈ʒu</summary>
    [SerializeField] Vector3 _vector3;

    /// <summary>���̕�</summary>
    [SerializeField] float _leftWall;
    /// <summary>�E�̕�</summary>
    [SerializeField] float _rightWall;
    /// <summary>�ړ�</summary>
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
        // ���@���ړ�������
        //float h = Input.GetAxisRaw("Horizontal");   // ���������̓��͂��擾����
        //float v = Input.GetAxisRaw("Vertical");     // ���������̓��͂��擾����
        //_dir = new Vector3(h, 0, 0).normalized; // �i�s�����̒P�ʃx�N�g������� (dir = direction) 
        //_dir = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.forward;

        //if (!isStop)
        //{
            //_rigidBody.velocity = _dir * m_moveSpeed; //�P�ʃx�N�g���ɃX�s�[�h�������đ��x�x�N�g���ɂ��āA����� Rigidbody �̑��x�x�N�g���Ƃ��ăZ�b�g����
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