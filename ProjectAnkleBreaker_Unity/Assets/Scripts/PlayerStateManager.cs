using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Got from the video https://www.youtube.com/watch?v=f3IYIvd-1mY

public partial class PlayerStateManager : MonoBehaviour

    
{
    //Variables
    [SerializeField] bool hasBall;
    private InputAction m_shootBall;
    private int playerID;
    //private AnimationSystem animSys;


    private void Awake()
    {
        playerID = 0;
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInput>();
        //animSys = GetComponent<Animator>();
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
        if (Mathf.Abs(MoveVector.x) > 0 || Mathf.Abs(MoveVector.y) > 0 || Mathf.Abs(MoveVector.z) > 0)
        {
            StartRunningAnim();
        }
        else 
        {
            StopRunningAnim();
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
    //State Variables
    public Animator animator;


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
        yield return new WaitForSeconds(3);
        animator.SetBool("hasBall", false);
        animator.SetBool("isShooting", false);

    }

    #endregion


}
