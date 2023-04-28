using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimAnimation : MonoBehaviour
{
    public Animator animator;
    private GameObject basketBall;
    //private GameObject player;
    public bool isDunking;

    // Start is called before the first frame update
    void Start()
    {
        basketBall = GameObject.Find("Basketball Model");
        //player = 
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the Player Has the ball.

        //Call Animator if Player Scores to Play animation.
        BasketballHandler bh = basketBall.GetComponent<BasketballHandler>();
        if (bh.shotEntered) 
        {
            StartCoroutine(PlayRegularAnimation());
        }
        //Play Stealing Ball
        
    }

    IEnumerator PlayRegularAnimation() 
    {
        animator.SetBool("exitHoop", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("exitHoop", false);

    }
    IEnumerator PlayDunkAnimation() 
    {
        animator.SetBool("exitHoop", true);
        animator.SetBool("isDunking", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("exitHoop", false);
        animator.SetBool("isDunking", false);
    }
}
