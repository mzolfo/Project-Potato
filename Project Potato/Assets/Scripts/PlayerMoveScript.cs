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
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Vector2 movementVector = new Vector2(horizontalInput * movementSpeed * Time.deltaTime, myRigidBody2d.velocity.y);
        myRigidBody2d.velocity = movementVector;
    }

    private void Jump()
    {
        Debug.Log("Player Has Jumped");
        Vector2 movementVector = new Vector2(myRigidBody2d.velocity.x, jumpImpulse * Time.deltaTime);
        myRigidBody2d.velocity = movementVector;
       // myRigidBody2d.AddForce(new Vector2(0,jumpImpulse), ForceMode2D.Impulse);
    }

}
