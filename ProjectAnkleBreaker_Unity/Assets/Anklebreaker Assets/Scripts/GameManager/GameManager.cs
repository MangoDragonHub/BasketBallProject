using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized] public int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreCheck()
    {
        if(score >= 21)
        {
            Debug.Log("The Game is Over!");
        }

    }

}
