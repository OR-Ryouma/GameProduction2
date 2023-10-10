using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アタッチしたGameObjectにCharacterControllerがアタッチされていない場合、アタッチする
[RequireComponent(typeof(CharacterController))]

public class PlayerController2 : MonoBehaviour
{
    // アタッチしているGameObjectのCharacterControllerを格納する変数
    private CharacterController _characterController;
    //Vector3の設定
    Vector3 _movedir = Vector3.zero;
    //自分のいるラインを格納する変数
    private int _lane;

    // ジャンプ力を設定（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private float _jumpPower = 10f;
    // ダウン力を設定（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private float _duwnPower = -20f;
    // X軸のスピードを設定（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private float _speedX = 1.0f;
    // Z軸のスピードを設定（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private float _speedZ = 1.0f;
    // 速度上昇値（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private float _acceleratorZ = 0.1f;

    // 重力を設定（Inspectorタブからも値を変更できるようにする）
    [SerializeField]
    private float _gravity = 10f;

    //ダメージ判定
    public bool _isDamage;

    //スライディング判定
    public bool _isSriding;

    //キャラ移動
    public static bool _stop;

    //Enemyと衝突用のRay
    //Rayの長さ
    [SerializeField] private float _rayLengthF = 1f;
    [SerializeField] private float _rayLengthD = 1f;
    //Rayの位置調整
    [SerializeField] private float _rayOffset;
    //Rayの判定用のLayer
    [SerializeField] private LayerMask _layMaskE = default;
    //Enemy検知
    private bool _isEnemyF;
    private bool _isEnemyD;
    private bool _isEnemy;

    int _hp = 1;

    // Start is called before the first frame update
    void Start()
    {
        // アタッチしているGameObjectのCharacterControllerを取得
        //（FixedUpdate()内で毎回取得すると処理が重くなるので、Start()で取得する
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!_stop)
        {
            // 左右とSpaceのキーの動作
            //上限と下限を決めておくことによりそれ以上左右に動かなくさせている
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

            //Mathf.Clamp関数は、第一引数の値を、第二引数と第三引数の間で収まるようにする処理。これにより徐々に加速し最高速度を超えないようにしている
            _movedir.z = Mathf.Clamp(_movedir.z + (_acceleratorZ * Time.deltaTime), 0, _speedZ);

            float _ratioX = (_lane * 2.0f - transform.position.x) / 1.0f;
            _movedir.x = _ratioX * _speedX;
            //重力の追加
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
        //接地判定
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
        //放つレイの初期位置
        var rayF = new Ray(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.forward);

        //Raycastがhitするかどうかで判定
        return Physics.Raycast(rayF, _rayLengthF, _layMaskE);
    }

    private bool CheckEnemyD()
    {
        var rayD = new Ray(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.down);

        return Physics.Raycast(rayD, _rayLengthD, _layMaskE);
    }

    //Debugように可視化させる
    private void OnDrawGizmos()
    {
        //接地判定時は緑、接地外は赤とする
        Gizmos.color = _isEnemyF ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.forward * _rayLengthF);
        Gizmos.color = _isEnemyD ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.down * _rayLengthD);
    }

}
