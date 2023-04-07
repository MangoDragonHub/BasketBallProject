using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Got from the video https://www.youtube.com/watch?v=f3IYIvd-1mY

public partial class PlayerStateManager : MonoBehaviour


{
    //Variables
    [SerializeField] BasketballHandler basketballHandler;
    public bool hasBall;
    public bool awayTeam;
    private int playerID;
    public Animator animator;
    private ThirdPersonController tpc;
    private PlayerInput pl_input;


    private void Awake()
    {
        playerID = 0;
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInput>();
        tpc = GetComponent<ThirdPersonController>();
        pl_input = GetComponent<PlayerInput>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Actions

    public void ShootBall()
    {
        StartCoroutine(ShootAnim());
    }

    public void PauseGame()
    {
        GameObject PauseMenu = GameObject.Find("GAME UI");
        PauseMenu.GetComponent<MainMenuOptions>().PauseGame();
    }

    public void StealBall()
    {
        //When the opponent wants to steal a ball from another, they take the ball immediately and push the opponent away.
        //To prevent the player that had the ball from obtaining the ball again the split second after they got pushed,
        //maybe have the collider of that player ignore the ball's collider for a set amount of time?
        Debug.Log("AP DEBUG: STEAL BUTTON has been pushed.");
        /*
        //Search for the player with the opponent's tag
        if (!hasBall)
        {
            StartCoroutine(tempDisableMovement());
            animator.Play("Offense_push"); //The code below causes the opponent to release the ball as they do the animation.
            
            if (this.tag == "Player2")
            {
                GameObject player1 = GameObject.FindGameObjectWithTag("Player");
                float distance = Vector3.Distance(player1.transform.position, transform.position);
                if (distance < 1.5)
                {
                    Animator p1_anim = player1.GetComponent<Animator>();
                    PlayerStateManager p1_psm = player1.GetComponent<PlayerStateManager>();
                    p1_anim.Play("Fall");
                    p1_psm.tempDisableMovementOnFall();
                    p1_psm.hasBall = false;
                    p1_anim.SetBool("hasBall", false);
                    basketballHandler.ReleaseFromPlayerHand();
                }
            }
            
        }
        */
        
    }

    public void DefendBall()
    {
        //This is where "Guard frames" will apply and will allow the player to protect the ball from being stolen by the opponent.
        //There will be a set amount of time where "guard frames" will apply so the player will be invulnerable.
         Debug.Log("AP DEBUG: DEFENSE BUTTON has been pushed.");
    }

    private void IgnoreCollisionWithBall()
    {
        //Temporarily disable the collision between the player and the ball. Will be useful
        //when the player does a falling animation, as well as after the shooting if the player
        //decides to do a close-ramge free throw.
    }

    #endregion


    #region Animations


    public void StartRunningAnim()
    {
        animator.SetBool("isMoving", true);
    }

    public void StopRunningAnim()
    {
        animator.SetBool("isMoving", false);

    }

    //Shooting animation is a Coroutine to delay the animation transition
    IEnumerator ShootAnim()
    {
        if (animator.GetBool("hasBall"))
        {
            basketballHandler.anim.enabled = false;
            basketballHandler.ForceChangeParentToPlayerHand();
            pl_input.enabled = false; //Disables the player input completely for a set amount of time. This is so that the player character does not move unrealistically when they shoot. For now, this also disables pausing.
            animator.SetBool("isShooting", true);
            yield return new WaitForSeconds(0.3f);
            tpc.AllowJump = true; //Do not call JumpAction() as its already in the update. This bool variable can handle when the character jumps.
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("hasBall", false);
            basketballHandler.ShootBall();
            yield return new WaitForSeconds(.8f);
            //shoot
            animator.SetBool("isShooting", false);
            pl_input.enabled = true;
            yield return new WaitForSeconds(.3f); //temporary bandaid fix. The coroutine is given 0.3 seconds to see if the player's
                                                  //animation state is still on "Shooting". If it is, then it will force the player
                                                  //to change the animation and if hasBall, relocates the ball to the player's
                                                  //hand.
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
            {
                if (animator.GetBool("hasBall"))
                {
                    animator.Play("Idle Dribble");
                    basketballHandler.FindRightHand();
                }
                if (!animator.GetBool("hasBall"))
                {
                    animator.Play("Idle");
                }
            }
        }
    }

    IEnumerator tempDisableMovement()
    {
        pl_input.enabled = false;
        yield return new WaitForSeconds(0.3f);
        pl_input.enabled = true;
    }

    IEnumerator tempDisableMovementOnFall()
    {
        pl_input.enabled = false;
        yield return new WaitForSeconds(1f);
        pl_input.enabled = true;
    }

    #endregion


    #region Audio

    public void PlayFootSound()
    {

    }




    #endregion

}
