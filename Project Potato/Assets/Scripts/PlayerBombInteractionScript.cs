using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombInteractionScript : MonoBehaviour
{

    private bool bombIsHeld;

    [SerializeField]
    private Transform playerArm;
    [SerializeField]
    private Transform bombHeldPosition;
    [SerializeField]
    private Transform thePlayer;

    private GameObject theBomb;
    private Rigidbody2D bombRigidBody;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (bombIsHeld)
            BombRotationHeld();
        if (Input.GetButtonDown("Keyboard_Throw"))
            ThrowBomb();
        //if (theBomb == null)
            //ebug.Log("Not holding the bomb");
    }

    private void ThrowBomb()
    {
        Debug.Log("Working");

        bombIsHeld = false;
        bombRigidBody.isKinematic = false;
        theBomb.transform.parent = null;

        
        //HOW DO WITH CONTROLLER?????!!?!?!?!
        Vector2 throwVector = Input.mousePosition;
        throwVector = Camera.main.ScreenToWorldPoint(throwVector) - transform.position;

        bombRigidBody.velocity = throwVector;
        //bombRigidBody.velocity = 100 * (bombRigidBody.velocity.normalized);

        theBomb.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void BombRotationHeld()
    {
        theBomb.transform.SetPositionAndRotation(bombHeldPosition.position, playerArm.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (!bombIsHeld)
            {
                theBomb = collision.gameObject;
                bombRigidBody = theBomb.GetComponent<Rigidbody2D>();
                PickupBomb();
            }
        }
    }

    private void PickupBomb()
    {
        bombIsHeld = true;
        //Should also turn off bomb detector since it can cause problems with throwing the bomb

        theBomb.transform.SetParent(thePlayer, false);

        bombRigidBody.isKinematic = true;

        theBomb.GetComponent<BoxCollider2D>().enabled = false;
    }
}
