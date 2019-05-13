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

    private void Start()
    {
        //bombHeldPosition = new Vector3(playerArm.position.x * -1, playerArm.position.y, playerArm.position.z);
    }

    private void Update()
    {
        if (bombIsHeld)
            BombRotationHeld();
    }

    private void BombRotationHeld()
    {
        theBomb.transform.SetPositionAndRotation(bombHeldPosition.position, playerArm.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Debug.Log("Bomb thing Working");

            if (!bombIsHeld)
            {
                theBomb = collision.gameObject;
                PickupBomb();
            }
        }
    }

    private void PickupBomb()
    {
        Rigidbody2D bombRB2D = theBomb.GetComponent<Rigidbody2D>();

        bombIsHeld = true;
        theBomb.transform.SetParent(thePlayer, false);

        bombRB2D.isKinematic = true;

        theBomb.GetComponent<BoxCollider2D>().enabled = false;
    }
}
