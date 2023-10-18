using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public GameObject _bullet;
    public GameObject _muzle;

    float up = 0.2f;
    float right = 0.1f;

    //jumpCount
    [SerializeField] private int _jcount = 0;
    [SerializeField] private float _timer;

    public int _hp = 5;

    // ���g��Transform
    [SerializeField] private Transform _self;

    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform _target;

    // �����^�̕ϐ����g���Ă܂��B
    Dictionary<string, bool> move = new Dictionary<string, bool>
    {
        {"right", false },
        {"left", false },
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move["right"] = Input.GetKey(KeyCode.RightArrow);
        move["left"] = Input.GetKey(KeyCode.LeftArrow);

        // �^�[�Q�b�g�̕����Ɏ��g����]������
        // ���������������v�Z
        Vector3 dir = (_target.position - _self.position);

        // �����Ō������������ɉ�]�����Ă܂�
        _self.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_jcount < 2)
            {
                StartCoroutine(Jump());
                _jcount++;

                if (Input.GetKeyUp(KeyCode.Space) && _jcount == 1)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        StopCoroutine(Jump());
                        StartCoroutine(Jump());
                        _jcount++;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(_bullet, _muzle.transform.position, transform.rotation);
        }

        if(_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (move["right"])
        {
            transform.Translate(right, 0f, 0f);
        }
        if (move["left"])
        {
            transform.Translate(-right, 0f, 0f);
        }
    }

    IEnumerator Jump()
    {
        for(int i = 0; i < 15; i++)
        {
            transform.Translate(0f, up, 0f);
            yield return new WaitForSeconds(_timer);
        }
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < 15; i++)
        {
            transform.Translate(0f, -up, 0f);
            yield return new WaitForSeconds(_timer);
        }

        _jcount = 0;
    }
}
