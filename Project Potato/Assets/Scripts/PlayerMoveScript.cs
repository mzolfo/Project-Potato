using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

    private float horizontalInput;
    [SerializeField]
    private float jumpImpulse;
    [SerializeField]
    private float movementSpeed;

    private Rigidbody2D myRigidBody2d;

    private bool onGround = false;
    private bool canJump = false;
    private int timesJumped = 0;

    private enum Joysticks {
        Keyboard = 1,
        Joy2 = 2
    };

    [SerializeField]
    private Joysticks joystickType;

    private string inputType;

    void Start()
    {
        if (joystickType == Joysticks.Keyboard)
            inputType = "Keyboard";
        if (joystickType == Joysticks.Joy2)
            inputType = "Joy2";
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown(inputType + "_Jump") && canJump)
        {
                Jump();
            Debug.Log("works");
        }
        Debug.Log("Times jumped: "+timesJumped);

            

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D()
    {
        onGround = true;
        canJump = true;
        timesJumped = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
    }

    private void Move()
    {
        horizontalInput = Input.GetAxisRaw(inputType + "_Horizontal");
        Vector2 movementVector = new Vector2(horizontalInput * movementSpeed * Time.deltaTime, myRigidBody2d.velocity.y);
        myRigidBody2d.velocity = movementVector;
        
    }

    private void Jump()
    {
        timesJumped++;
        Vector2 movementVector = new Vector2(myRigidBody2d.velocity.x, jumpImpulse * Time.deltaTime);
        myRigidBody2d.velocity = movementVector;
        if (timesJumped >= 2)
        {
            canJump = false;
        }
        
    }

}
