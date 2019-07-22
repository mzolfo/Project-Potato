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
    [SerializeField]
    private Transform throwVectorPos;

    [SerializeField]
    private float throwSpeed;
    private float tempTimer = -1;
    private string inputType;

    private GameObject theBomb;
    private Rigidbody2D bombRigidBody;

    private void Start()
    {
        PlayerController myPlayerController = GetComponentInParent<PlayerController>();
        if (myPlayerController.joystickType == Joysticks.Keyboard)
            inputType = "Keyboard";
        else if (myPlayerController.joystickType == Joysticks.Joy2)
            inputType = "Joy2";
    }

    private void Update()
    {
        if (bombIsHeld)
            BombRotationHeld();

        if (Input.GetButtonDown(inputType + "_Throw") && bombIsHeld)
            ThrowBomb();

        if (Time.time >= tempTimer + .05f && tempTimer != -1)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            tempTimer = -1;
        }
    }

    private void ThrowBomb()
    {
        //Disable box collider temporarily
        tempTimer = Time.time;
        GetComponent<BoxCollider2D>().enabled = false;

        theBomb.GetComponent<BombStatusScript>().ThrownBomb();

        bombIsHeld = false;
        bombRigidBody.isKinematic = false;
        theBomb.transform.parent = null;

        Vector2 throwVector = throwVectorPos.position - theBomb.transform.position;
        bombRigidBody.velocity = throwVector * throwSpeed;

        theBomb.GetComponent<BoxCollider2D>().enabled = true;

        theBomb = null;
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
        theBomb.GetComponent<BombStatusScript>().ResetBomb(transform.parent);
        theBomb.transform.SetParent(thePlayer, false);
        bombRigidBody.isKinematic = true;
        theBomb.GetComponent<BoxCollider2D>().enabled = false;
    }
}
