using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �A�^�b�`����GameObject��CharacterController���A�^�b�`����Ă��Ȃ��ꍇ�A�A�^�b�`����
[RequireComponent(typeof(CharacterController))]

public class PlayerController2 : MonoBehaviour
{
    // �A�^�b�`���Ă���GameObject��CharacterController���i�[����ϐ�
    private CharacterController _characterController;
    //Vector3�̐ݒ�
    Vector3 _movedir = Vector3.zero;
    //�����̂��郉�C�����i�[����ϐ�
    private int _lane;

    // �W�����v�͂�ݒ�iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private float _jumpPower = 10f;
    // �_�E���͂�ݒ�iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private float _duwnPower = -20f;
    // X���̃X�s�[�h��ݒ�iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private float _speedX = 1.0f;
    // Z���̃X�s�[�h��ݒ�iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private float _speedZ = 1.0f;
    // ���x�㏸�l�iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private float _acceleratorZ = 0.1f;

    // �d�͂�ݒ�iInspector�^�u������l��ύX�ł���悤�ɂ���j
    [SerializeField]
    private float _gravity = 10f;

    //�_���[�W����
    public bool _isDamage;

    //�X���C�f�B���O����
    public bool _isSriding;

    //�L�����ړ�
    public static bool _stop;

    //Enemy�ƏՓ˗p��Ray
    //Ray�̒���
    [SerializeField] private float _rayLengthF = 1f;
    [SerializeField] private float _rayLengthD = 1f;
    //Ray�̈ʒu����
    [SerializeField] private float _rayOffset;
    //Ray�̔���p��Layer
    [SerializeField] private LayerMask _layMaskE = default;
    //Enemy���m
    private bool _isEnemyF;
    private bool _isEnemyD;
    private bool _isEnemy;

    int _hp = 1;

    // Start is called before the first frame update
    void Start()
    {
        // �A�^�b�`���Ă���GameObject��CharacterController���擾
        //�iFixedUpdate()���Ŗ���擾����Ə������d���Ȃ�̂ŁAStart()�Ŏ擾����
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!_stop)
        {
            // ���E��Space�̃L�[�̓���
            //����Ɖ��������߂Ă������Ƃɂ�肻��ȏ㍶�E�ɓ����Ȃ������Ă���
            if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                if (_lane > -1f)
                {
                    _lane--;
                }
            }

            if ((Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.RightArrow)))
            {
                if (_lane < 1f)
                {
                    _lane++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_characterController.isGrounded)
                {
                    _movedir.y = _jumpPower;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!_isSriding)
                {
                    _isSriding = true;
                    _movedir.y = _duwnPower;
                }
            }

            //Mathf.Clamp�֐��́A�������̒l���A�������Ƒ�O�����̊ԂŎ��܂�悤�ɂ��鏈���B����ɂ�菙�X�ɉ������ō����x�𒴂��Ȃ��悤�ɂ��Ă���
            _movedir.z = Mathf.Clamp(_movedir.z + (_acceleratorZ * Time.deltaTime), 0, _speedZ);

            float _ratioX = (_lane * 2.0f - transform.position.x) / 1.0f;
            _movedir.x = _ratioX * _speedX;
            //�d�͂̒ǉ�
            _movedir.y -= _gravity * Time.deltaTime;

            Vector3 _globalgir = transform.TransformDirection(_movedir);
            _characterController.Move(_globalgir * Time.deltaTime);
        }

        if (_characterController.isGrounded)
        {
            _movedir.y = 0f;
        }

        if (_isEnemy)
        {
            _isDamage = true;
            _isEnemy = false;
        }

        if (_isDamage)
        {
            _movedir.z = 0;
            _hp = _hp - 1;
            if (_hp <= 0)
            {
                _stop = true;
                //GamesManager._instanceGames._clear = false;
            }
            _isDamage = false;
        }

    }

    private void FixedUpdate()
    {
        //�ڒn����
        _isEnemyF = CheckEnemyF();
        _isEnemyD = CheckEnemyD();

        if (_isEnemyF || _isEnemyD)
        {
            _isEnemy = true;
        }
        else
        {
            _isEnemy = false;
        }
    }

    private bool CheckEnemyF()
    {
        //�����C�̏����ʒu
        var rayF = new Ray(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.forward);

        //Raycast��hit���邩�ǂ����Ŕ���
        return Physics.Raycast(rayF, _rayLengthF, _layMaskE);
    }

    private bool CheckEnemyD()
    {
        var rayD = new Ray(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.down);

        return Physics.Raycast(rayD, _rayLengthD, _layMaskE);
    }

    //Debug�悤�ɉ���������
    private void OnDrawGizmos()
    {
        //�ڒn���莞�͗΁A�ڒn�O�͐ԂƂ���
        Gizmos.color = _isEnemyF ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.forward * _rayLengthF);
        Gizmos.color = _isEnemyD ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.down * _rayLengthD);
    }

}
