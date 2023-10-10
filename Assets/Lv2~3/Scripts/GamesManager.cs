using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesManager : MonoBehaviour
{
    public static GamesManager _instanceGames = null;

    //ÉXÉRÉA
    public int _score;

    //ãóó£
    public float _distance;

    public bool _playerDamege;
    public bool _clear;

    private void Awake()
    {
        if (_instanceGames == null)
        {
            _instanceGames = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
