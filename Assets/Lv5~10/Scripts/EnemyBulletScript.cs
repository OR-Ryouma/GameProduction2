using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    GameObject _player;

    bool att;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.1f, 0f, 0f);

        if (transform.position.x < -9.5f)
        {
            Destroy(gameObject);
        }

        //当たり判定
        //プレイヤーの半径
        float rp = 0.5f;
        //プレイヤーの中心座標
        float xc = _player.transform.position.x;
        float yc = _player.transform.position.y;
        //弾の形
        float x1 = transform.position.x - 0.4f;
        float y1 = transform.position.y + 0.3f;
        float x2 = transform.position.x + 0.4f;
        float y2 = transform.position.y - 0.3f;

        bool A = false;
        bool B = false;
        bool C = false;
        bool D = false;
        bool E = false;
        bool F = false;

        if (xc > x1 && xc < x2 && yc < y1 + rp && yc > y2 - rp)
        {
            A = true;
        }
        if(xc > x1 - rp && xc < x2 + rp && yc < y1 && yc > y2)
        {
            B = true;
        }
        if((x1 - xc) * (x1 - xc) + (y1 - yc) * (y1 - yc) < rp * rp)
        {
            C = true;
        }
        if ((x2 - xc) * (x2 - xc) + (y1 - yc) * (y1 - yc) < rp * rp)
        {
            D = true;
        }
        if ((x2 - xc) * (x2 - xc) + (y2 - yc) * (y2 - yc) < rp * rp)
        {
            E = true;
        }
        if ((x1 - xc) * (x1 - xc) + (y2 - yc) * (y2 - yc) < rp * rp)
        {
            F = true;
        }

        if(A || B || C || D || E || F)
        {
            att = true;
        }

        if(att)
        {
            PlayerController2D playerController2D = _player.GetComponent<PlayerController2D>();
            playerController2D._hp--;
            int hp = playerController2D._hp;
            Debug.Log(hp);
            Destroy(gameObject);
        }
    }
}
