using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJumpingManager : MonoBehaviour
{
    private CharacterController cc;

    //Jump vars
    private Vector3 velocity;
    private bool grounded;
    [SerializeField] private float jumpHeight = 2.0f;
    private float gravityValue = -0f;
    public bool AllowedToJump;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JumpAction();
    }

    void JumpAction()
    {
        if (AllowedToJump == false)
        {
            //if the player is on the ground, do NOT change velocity, stop vertical movement.
            velocity.y = 0f;
        }
        else
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -1f * gravityValue);
            AllowedToJump = false;
        }
        velocity.y += gravityValue * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
