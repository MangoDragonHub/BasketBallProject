using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized] public int score;
    public int gameScoreCap;
    private MainMenuOptions _mainMenuOptions;

    // Start is called before the first frame update
    void Start()
    {
        if (gameScoreCap == null)
        {
            gameScoreCap = 21;
        }

        score = 0;
        _mainMenuOptions = GameObject.Find("UI").GetComponent<MainMenuOptions>();

    }

    // Update is called once per frame
    void Update()
    {
        ScoreCheck();
    }

    private void ScoreCheck()
    {
        if(score >= gameScoreCap)
        {
            //Debug.Log("The Game is Over!");
            _mainMenuOptions.EndGameScreen();
            
        }

    }

}
