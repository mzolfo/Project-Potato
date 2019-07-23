using System;
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
    [SerializeField]
    private float dodgeImpulse;

    private Rigidbody2D myRigidBody2d;

<<<<<<< HEAD:Project Potato/Assets/Scripts/PlayerMoveScript.cs
    private bool onGround = false;
=======
    private bool jump = false;
    private bool dodge = false;
>>>>>>> master:Project Potato/Assets/Scripts/PlayerController.cs
    private bool canJump = false;
    private bool canDodge = true;
    private bool dodgeRight;

    private int timesJumped = 0;
    private float dodgeCooldown;

    public enum Joysticks {
        Keyboard = 1,
        Joy2 = 2
    };

    //[SerializeField]
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
                Jump();
            //Debug.Log("works");
        }
<<<<<<< HEAD:Project Potato/Assets/Scripts/PlayerMoveScript.cs
        Debug.Log("Times jumped: "+timesJumped);
=======

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
>>>>>>> master:Project Potato/Assets/Scripts/PlayerController.cs
    }


    private void FixedUpdate()
    {
        Move();
<<<<<<< HEAD:Project Potato/Assets/Scripts/PlayerMoveScript.cs
=======
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
>>>>>>> master:Project Potato/Assets/Scripts/PlayerController.cs
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("Floor found");
            onGround = true;
            canJump = true;
            timesJumped = 0;
        }
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
