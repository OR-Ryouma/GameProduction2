using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //�ł��߂��G
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
            //�o�ߎ��Ԃ̍폜
            searchTime = 0;
        }

        //�����蔻��
        //�G�̔��a
        float rp = 0.5f;
        //�G�̒��S���W
        float xc = _enemy.transform.position.x;
        float yc = _enemy.transform.position.y;
        //�e�̌`
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

    //�w�肳�ꂽ�^�O�̒��ōł��߂����̂��擾
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;   //�����p�ꎟ�ϐ�
        float nearDis = 0;  //�ł��߂�Object�̋���
        //string nearObjName = "";  //object����
        GameObject targetObj = null;    //object

        //�^�O�w�肳�ꂽObject��z��Ŏ擾����
        foreach(GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //���g�Ǝ擾����Object�̋������擾
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //Object�̋�������ԋ߂����A����0�ł����Object�Ɩ����擾
            //�ꎞ�ϐ��ɋ������i�[
            if(nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObject = obj.name;
                targetObj = obs;
            }
        }
        //�ł��߂�����Object��T��
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
