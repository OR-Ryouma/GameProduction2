using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //最も近い敵
    public GameObject _enemy;
    private float searchTime = 0;

    bool att;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = serchTag(gameObject, "Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.1f, 0f,0f);

        if(transform.position.x > 9.5f)
        {
            Destroy(gameObject);
        }

        searchTime += Time.deltaTime;

        if(searchTime >= 0.01f)
        {
            _enemy = serchTag(gameObject, "Enemy");
            //経過時間の削除
            searchTime = 0;
        }

        //当たり判定
        //敵の半径
        float rp = 0.5f;
        //敵の中心座標
        float xc = _enemy.transform.position.x;
        float yc = _enemy.transform.position.y;
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
        if (xc > x1 - rp && xc < x2 + rp && yc < y1 && yc > y2)
        {
            B = true;
        }
        if ((x1 - xc) * (x1 - xc) + (y1 - yc) * (y1 - yc) < rp * rp)
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

        if (A || B || C || D || E || F)
        {
            att = true;
        }

        if (att)
        {
            EnemyScript enemyScript = _enemy.GetComponent<EnemyScript>();
            enemyScript._enemyhp--;
            int enemyhp = enemyScript._enemyhp;
            Debug.Log(enemyhp);
            Destroy(gameObject);
        }
    }

    //指定されたタグの中で最も近いものを取得
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;   //距離用一次変数
        float nearDis = 0;  //最も近いObjectの距離
        //string nearObjName = "";  //object名称
        GameObject targetObj = null;    //object

        //タグ指定されたObjectを配列で取得する
        foreach(GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したObjectの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //Objectの距離が一番近いか、距離0であればObjectと名を取得
            //一時変数に距離を格納
            if(nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObject = obj.name;
                targetObj = obs;
            }
        }
        //最も近かったObjectを探す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
