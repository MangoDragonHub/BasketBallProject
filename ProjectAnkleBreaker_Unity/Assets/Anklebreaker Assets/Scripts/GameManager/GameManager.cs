using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum GameMode
{
    ScoreAttack,
    Competitive,
    Practice
}

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized] public int scoreP1;
    [System.NonSerialized] public int scoreP2;
    public int gameScoreCap;
    public int gameClock;
    private float remainingTime;
    private MainMenuOptions _mainMenuOptions;
    //private TextMeshProUGUI finalScore;
    public TextMeshProUGUI clockLabelRef;
    private TextMeshProUGUI finalScore;
    public GameMode setGameMode;
    public Selectable primaryButton;

    // Start is called before the first frame update
    void Start()
    {
        ModeSelection(setGameMode);
        scoreP1 = 0;
        scoreP2 = 0;
        remainingTime = gameClock;
        _mainMenuOptions = GameObject.Find("GAME UI").GetComponent<MainMenuOptions>();
        if (clockLabelRef == null)
        {

            clockLabelRef = GameObject.Find("Clock").GetComponent<TextMeshProUGUI>();
        }
        //finalScore = GameObject.Find("FinalScore").GetComponent<TextMeshProUGUI>();





    }

    // Update is called once per frame
    void Update()
    {
        ScoreCheck();
        GameClock();
    }

    private void ModeSelection(GameMode modeSelection) 
    {
        switch (modeSelection) 
        {
            case GameMode.ScoreAttack:
                if (gameScoreCap == 0)
                {
                    gameScoreCap = 999;
                }
                break;
            case GameMode.Competitive:
                if (gameScoreCap == 0) 
                {
                    gameScoreCap = 21;
                }
                break;
            case GameMode.Practice:
                if (gameScoreCap == 0)
                {
                    gameScoreCap = 9999;
                }
                break;
            default:
                if (gameScoreCap == null || gameScoreCap == 0)
                {
                    gameScoreCap = 21;
                }
                break;
        }
    }

    private void ScoreCheck()
    {
        if(scoreP1 >= gameScoreCap || scoreP2 >= gameScoreCap)
        {
            //Debug.Log("The Game is Over!");
            if (_mainMenuOptions != null) 
            {
                StartCoroutine(GameOver());
            }
           
            
        }


        //Debug.Log(scoreP1);
        //Double check if score resets and not affect the game.
        if (setGameMode == GameMode.Competitive) 
        {
            if (scoreP1 >= 21 || scoreP2 >= 21)
            {
                scoreP1 = 0;
                scoreP2 = 0;
            }

        }
        if (setGameMode == GameMode.Practice)
        {
            if (scoreP1 >= 9999 || scoreP2 >= 9999)
            {
                scoreP1 = 0;
                scoreP2 = 0;
            }

        }

    }

    private void GameClock() 
    {
       

        //Check is the game clock is on screen.
        if (gameClock != null) 
        {
            gameClock = 60;
            remainingTime -= Time.deltaTime;

            if (clockLabelRef != null)
            {
                clockLabelRef.text = remainingTime.ToString("F0");
            }
            else
            {
                Debug.Log("clockLabelRef is null");
            }

        }

        if (remainingTime <= 0)
        {
            //Checks if the end game menu is available
            if (_mainMenuOptions != null)
            {
                _mainMenuOptions.EndGameScreen();
            }

            if (finalScore != null)
            {
                finalScore.text = scoreP1.ToString();
            }
            else
            {
                Debug.Log("Final_Score.tesxt is null");
            }
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

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        _mainMenuOptions.EndGameScreen();


    }

    void OnEnable()
    {
        primaryButton.Select();
    }
}
