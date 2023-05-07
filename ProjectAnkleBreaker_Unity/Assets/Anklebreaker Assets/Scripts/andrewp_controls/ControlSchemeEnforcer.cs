using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.XR;

public class ControlSchemeEnforcer : MonoBehaviour
{
    //Feel free to disable this Component via inspector in case you need to test using the Keyboard.

    public PlayerInput player1Input;
    public PlayerInput player2Input;
    // Start is called before the first frame update
    void Start()
    {
        //EnforceSchemes();
    }

    private void Update()
    {
        EnforceSchemes();
    }

    private void EnforceSchemes()
    {
        /*
        if (!player1psm.pl_input.enabled || !player2psm.pl_input.enabled)
        {
            return; //so null user errors aren't sharted out when you guard or shoot.
        }
        */
        player1Input.SwitchCurrentControlScheme("Gamepad", InputSystem.GetDevice("XInputControllerWindows"));
        player2Input.SwitchCurrentControlScheme("Gamepad", InputSystem.GetDevice("XInputControllerWindows1"));
    }
}
