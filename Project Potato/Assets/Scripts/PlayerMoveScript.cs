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

    void Start()
    {
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
                Jump();
        }
        Debug.Log("Times jumped: "+timesJumped);

        //if (!onGround)
        //{
        //    if (Physics2D.OverlapCircle(transform.position, 2f))
        //    {
        //        myRigidBody2d.velocity = new Vector2(0, myRigidBody2d.velocity.y);
        //        Debug.Log("It's working");
                
        //    }
        //}
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
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
