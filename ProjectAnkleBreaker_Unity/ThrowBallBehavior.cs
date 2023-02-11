using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowBallBehavior : MonoBehaviour
{

    private PlayerInput ap_iaa;
    private InputAction inputActions;
    private PlayerStateManager PSM;


    private void Awake()
    {
        PSM = GetComponent<PlayerStateManager>();
        inputActions = GetComponent<InputAction>();
    }

    void OnShoot()
    {
        PSM.ShootBall();
    }

    void OnPause()
    {
        PSM.PauseGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
