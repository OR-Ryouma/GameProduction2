using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text _score = null;
    private int _oldScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        _score = GetComponent<Text>();

        if (GamesManager._instanceGames != null)
        {
            _score.text = GamesManager._instanceGames._score.ToString();
        }
        else
        {
            Debug.Log("GamesManager‚Ì’u‚«–Y‚ê");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_oldScore != GamesManager._instanceGames._score)
        {
            _score.text = GamesManager._instanceGames._score.ToString();

            _oldScore = GamesManager._instanceGames._score;
        }
    }
}