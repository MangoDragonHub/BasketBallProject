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
    private InputAction m_shootBall;
    private int playerID;
    public Animator animator;


    private void Awake()
    {
        playerID = 0;
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInput>();
        PlayerSpeed = 10f;
        PlayerRotateSpeed = 180;

        _gravityVector = new Vector3(0, -9.81f, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateTowardsVector();
        ApplyGravity();
        ShootBall();
    }

    #region Movement
    public void ApplyGravity()
    {
        Controller.Move(_gravityVector * Time.deltaTime);
    }

    public void Move()
    {
        Controller.Move(PlayerSpeed * MoveVector * Time.deltaTime);
        //Play Animation
        if (Mathf.Abs(MoveVector.x) > 0 || Mathf.Abs(MoveVector.y) > 0 || Mathf.Abs(MoveVector.z) > 0)
        {
            StartRunningAnim();
        }
        else 
        {
            StopRunningAnim();
        }

        //Change Animation Layer weight if the player has the ball
        if (hasBall)
        {
            animator.SetLayerWeight(1,1);
        }
        else 
        {
            animator.SetLayerWeight(1,0);
        }
    }

    

    public void RotateTowardsVector()
    {
        var xzDirection = new Vector3(MoveVector.x, 0, MoveVector.z);
        if (xzDirection.magnitude == 0) return;

        var rotation = Quaternion.LookRotation(xzDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, PlayerRotateSpeed);

    }
    #endregion

    #region Actions

    public void ShootBall() 
    {
        m_shootBall = Input.actions["Shoot"];
        if (m_shootBall.triggered) 
        {
            StartCoroutine(ShootAnim());
             
        }
        
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
        yield return new WaitForSeconds(2.2f);
        animator.SetBool("hasBall", false);
        basketballHandler.ShootBall();
        yield return new WaitForSeconds(.8f);
        //shoot
        animator.SetBool("isShooting", false);

    }

    #endregion


}
