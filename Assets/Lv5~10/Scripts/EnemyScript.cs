using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject _enemyBullet;
    GameObject _player;
    Vector2 _transform;
    private float _timer = 0;

    public int _enemyhp = 3; 

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform.position;
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if((_transform.x - 9f) < _player.transform.position.x && (_transform.x + 9f) > _player.transform.position.x)
        {
            if((_transform.y - 3.5f) < _player.transform.position.y && (_transform.y + 3.5f) > _player.transform.position.y)
            {
                _timer += Time.deltaTime;

                if(_timer >= 1f)
                {
                    Instantiate(_enemyBullet, transform.position, Quaternion.identity);
                    _timer = 0;
                }
            }
        }

        if(_enemyhp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
