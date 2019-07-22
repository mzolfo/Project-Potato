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

    private Rigidbody2D myRigidBody2d;

    private bool jump = false;
    private bool canJump = false;
    private int timesJumped = 0;

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
        Debug.Log("Times jumped: " + timesJumped);
    }


    private void FixedUpdate()
    {
        Move();
        if (jump == true)
        {
            jump = false;
            Jump();          
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

}
