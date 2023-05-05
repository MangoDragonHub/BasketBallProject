using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.XR;

public class ControlSchemeEnforcer : MonoBehaviour
{
    public PlayerStateManager player1psm;
    public PlayerStateManager player2psm;
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
            player1psm.pl_input.SwitchCurrentControlScheme("Gamepad", InputSystem.GetDevice("XInputControllerWindows"));
            player2psm.pl_input.SwitchCurrentControlScheme("Gamepad", InputSystem.GetDevice("XInputControllerWindows1"));
    }
}
