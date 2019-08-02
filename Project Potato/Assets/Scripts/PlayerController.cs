using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Joysticks
{
    Keyboard,
    Joy2
};
public class PlayerController : MonoBehaviour
{

    private float horizontalInput;
    [SerializeField]
    private float jumpImpulse;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float dodgeImpulse;

    private Rigidbody2D myRigidBody2d;

    private bool jump = false;
    private bool dodge = false;
    private bool canJump = false;
    private bool canDodge = true;
    private bool dodgeRight;

    private int timesJumped = 0;
    private float dodgeCooldown;

    public Joysticks joystickType;

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
            //This needs to be in both here and fixed update or physics get wonky on different hardware, also if it's all in fixed theres delay in hitting the jump btn and it working
            jump = true;
        }

        if (Input.GetButtonDown(inputType + "_Dodge") && canDodge)
        {
            Debug.Log("dodge button down");
            dodge = true;
            dodgeCooldown = Time.time;
        }

        if (!canDodge && Time.time >= dodgeCooldown + 1f)
        {
            canDodge = true;
        }

        if (Input.GetAxisRaw(inputType + "_Horizontal") > 0)
        {
            dodgeRight = true;
        }
        else if (Input.GetAxisRaw(inputType + "_Horizontal") < 0)
        {
            dodgeRight = false;
        }

        //Debug.Log("Times jumped: " + timesJumped);
    }


    private void FixedUpdate()
    {
        Move();
        if (jump)
        {
            jump = false;
            Jump();          
        }
        if (dodge)
        {
            canDodge = false;
            dodge = false;
            Dodge();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("Floor found"); //BUG will detect the floor if you hit the bottom of it, i'm not sure whyt this is happening when the trigger is nowhere near the floor
            canJump = true;
            timesJumped = 0;
        }
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
        Vector2 movementVector = new Vector2(myRigidBody2d.velocity.x, jumpImpulse * Time.deltaTime * 100); // The * 100 just makes it simpler to adjust in the editor
        myRigidBody2d.velocity = movementVector;
        if (timesJumped >= 2)
        {
            canJump = false;
        }
        
    }

    private void Dodge()
    {
        Debug.Log("Dodge fuction activated");
        horizontalInput = Input.GetAxisRaw(inputType + "_Horizontal");
        Vector2 movementVector = new Vector2(horizontalInput * dodgeImpulse * 100, myRigidBody2d.velocity.y);
        myRigidBody2d.AddForce(movementVector);
        //myRigidBody2d.velocity = movementVector;
        //myRigidBody2d.AddRelativeForce(movementVector);
        
    }

}
