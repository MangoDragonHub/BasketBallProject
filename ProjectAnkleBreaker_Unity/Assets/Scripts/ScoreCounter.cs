using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private GameManager GM;
    private TextMeshProUGUI uiScoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        //basketballHandler = GameObject.Find("Basketball Model").GetComponent<BasketballHandler>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        uiScoreCounter = this.GetComponent<TextMeshProUGUI>();
    }

    
    void FixedUpdate()
    {

        //Update UI
        uiScoreCounter.text = GM.score.ToString();

    }
}
