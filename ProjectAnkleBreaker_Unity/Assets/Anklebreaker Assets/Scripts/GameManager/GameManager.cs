using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized] public int scoreP1;
    [System.NonSerialized] public int scoreP2;
    public int gameScoreCap;
    private MainMenuOptions _mainMenuOptions;

    // Start is called before the first frame update
    void Start()
    {
        if (gameScoreCap == null)
        {
            gameScoreCap = 21;
            
        }

        scoreP1 = 0;
        scoreP2 = 0;
        _mainMenuOptions = GameObject.Find("UI").GetComponent<MainMenuOptions>();

    }

    // Update is called once per frame
    void Update()
    {
        ScoreCheck();
    }

    private void ScoreCheck()
    {
        if(scoreP1 >= gameScoreCap || scoreP2 >= gameScoreCap)
        {
            //Debug.Log("The Game is Over!");
            _mainMenuOptions.EndGameScreen();
            
        }

    }


    public void Score_player_one(int add_score)
    {
        Debug.Log("Player_One_update");
        scoreP1 = scoreP1 + add_score;
    }

    public void Score_player_two(int add_score)
    {
        Debug.Log("Player_Two_update");
        scoreP2 = scoreP2 + add_score;
    }

}
