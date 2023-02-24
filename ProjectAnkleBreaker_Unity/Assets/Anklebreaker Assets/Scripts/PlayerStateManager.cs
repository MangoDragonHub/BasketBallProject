using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Got from the video https://www.youtube.com/watch?v=f3IYIvd-1mY

public partial class PlayerStateManager : MonoBehaviour

    
{
    //Variables
    [SerializeField] BasketballHandler basketballHandler;
    [SerializeField] bool hasBall;
    private int playerID;
    public Animator animator;


    private void Awake()
    {
        playerID = 0;
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInput>();
        //PlayerSpeed = 10f;
        //PlayerRotateSpeed = 180;

        _gravityVector = new Vector3(0, -9.81f, 0);
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
        basketballHandler.ChangeParentToPlayerHand(); //This method disables the Animator component for the ball and changes its parent to the player's hand instead of the Attach Point.
        StartCoroutine(ShootAnim());
    }

    public void PauseGame() 
    {
        GameObject PauseMenu = GameObject.Find("UI");
        PauseMenu.GetComponent<MainMenuOptions>().PauseGame();
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
        animator.SetBool("isShooting", true);
            yield return new WaitForSeconds(0.7f);
            animator.SetBool("hasBall", false);
            basketballHandler.ShootBall();
            yield return new WaitForSeconds(.8f);
            //shoot
            animator.SetBool("isShooting", false);

    }

    #endregion


    #region Audio

    public void PlayFootSound()
    {

    }




    #endregion

}
