using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballHandler : MonoBehaviour
{
    public bool hasBall;
    public GameObject playerHand;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hasBall = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if Player has the ball or not.
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //Connects to player's Animator to play Dribble Animation
            hasBall = true;
            Animator playerAnims = player.GetComponent<Animator>();
            playerAnims.SetBool("hasBall", true);
            //Debug to tell who has the ball
            Debug.Log($"{player} has the ball");

            //Attaches Ball to Player
            SetParent(playerHand);
        }

    }

    public void SetParent(GameObject newParent) 
    {
        //Sets the Game Object as a child to the object it want to be parented
        playerHand.transform.parent = newParent.transform;
        //Console Check
        Debug.Log("Player's Parent: " + playerHand.transform.parent.name);
    }

    public void DetachFromParent() 
    {
        //Detaches ball from Player's hand
        transform.parent = null;
    }

}
