using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombStatusScript : MonoBehaviour
{
    //We do need this

    private Rigidbody2D myRB2D;
    private Transform targetPlayer;
    [SerializeField]
    private Text bombTimerText;

    private bool isThrown = false;
    private bool speedUp = false;
    private bool timerOn = false;
    [SerializeField]
    private float maxReturnSpeed;
    private float returnSpeed;
    private float speedUpTimer;
    [SerializeField]
    private float bombTimerStart;
    private float bombTimer;

    private void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
        bombTimer = bombTimerStart;
    }

    private void Update()
    {
        if (timerOn && bombTimer >= .001)
        {
            bombTimer -= Time.deltaTime;
            bombTimerText.text = bombTimer.ToString("F2");
        }
        else if (!isThrown && bombTimer <= 0)
        {
            timerOn = false;
            BombExplodes();
        }
    }

    private void FixedUpdate()
    {
        if (isThrown == true && targetPlayer != null)
        {
            MoveTowardsTarget();
            if (Time.time >= speedUpTimer + .5f && returnSpeed != 1 && !speedUp)
            {
                returnSpeed--;
                speedUpTimer = Time.time;
            }
            else if (Time.time >= speedUpTimer + .5f && returnSpeed != maxReturnSpeed) // should only be called if the first doesnt happen, may have to change this 
            {
                speedUp = true;
                returnSpeed++;
                speedUpTimer = Time.time;
            }
        }
    }

    private void MoveTowardsTarget()
    {
        Debug.Log("Return speed: " + returnSpeed);
        Vector2 movementVector = new Vector2();

        if (!speedUp)
        {
            movementVector = (targetPlayer.position - transform.position) / returnSpeed;
        }
        else
        {
            movementVector = (targetPlayer.position - transform.position) * returnSpeed;
        }

        myRB2D.AddForce(movementVector);
    }

    public void ResetBomb(Transform target)
    {
        isThrown = false;
        speedUp = false;
        returnSpeed = maxReturnSpeed;

        if (target == targetPlayer)
        {
            Debug.Log("Target has not changed");
        }
        else
        {
            timerOn = true;
            targetPlayer = target;
            Debug.Log("Bomb is currently attached to: " + targetPlayer);
        }
    }

    public void ThrownBomb()
    {
        isThrown = true;
        myRB2D.freezeRotation = false;
        myRB2D.gravityScale = .1f;
        speedUpTimer = Time.time;
    }

    private void BombExplodes()
    {
        // HaloGruntHeadshotSound.mp3
        Debug.Log("BOOOOOM");
        Destroy(gameObject);
        Destroy(targetPlayer.gameObject);
    }

}
