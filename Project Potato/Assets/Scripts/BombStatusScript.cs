using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStatusScript : MonoBehaviour
{
<<<<<<< HEAD
    //MAy not be needed but will keep just in case
=======
    //We do need this

    private Rigidbody2D myRB2D;
    private Transform targetPlayer;
    [SerializeField]
    public bool isThrown = false;
    private bool speedUp = false;
    [SerializeField]
    private float maxReturnSpeed;
    private float returnSpeed;
    private float speedUpTimer;

    private GameMasterScript theGM;
    [SerializeField]
    private Transform player1Trans;
    [SerializeField]
    private Transform player2Trans;

    int[] array = new int[]{1,2,3,4,5,6,7,8,9,10};
    private List<int> list = new List<int> { };

    private void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
        theGM = GameMasterScript._instance;
        player1Trans = GameObject.FindGameObjectWithTag("Player 1").transform;
        player2Trans = GameObject.FindGameObjectWithTag("Player 2").transform;

        list.AddRange(array);
        
    }

    private void Update()
    {
        if (player1Trans == null)
            player1Trans = GameObject.FindGameObjectWithTag("Player 1").transform;
        if (player2Trans == null)
            player2Trans = GameObject.FindGameObjectWithTag("Player 2").transform;

        if (targetPlayer == null)
        {
            //float randomStart = Mathf.Round(Random.Range(1, 2));
            
            if (GetRandomStart(true) < 5)
                ResetBomb(player1Trans);
            else
                ResetBomb(player2Trans);
            isThrown = true;
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

    int GetRandomStart(bool reloadEmptyList)
    {
        if (list.Count == 0)
        {
            if (reloadEmptyList)
            {
                list.AddRange(array);
            }
            else
                return -1;
        }
        int rand = Random.Range(1, list.Count);
        int value = list[rand];
        list.RemoveAt(rand);
        return value;

    }

    private void MoveTowardsTarget()
    {
        //Debug.Log("Return speed: " + returnSpeed);
        Vector2 movementVector = new Vector2();

        if (!speedUp)
        {
            movementVector = (targetPlayer.position - transform.position) / returnSpeed * 2;
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
            theGM.timerOn = true;
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

    public void BombExplodes()
    {
        // HaloGruntHeadshotSound.mp3
        GameMasterScript.gameOver = true; // This is for early build only, better win mechanics will be implemented
        Debug.Log("BOOOOOM");
        Destroy(gameObject);
        Destroy(targetPlayer.gameObject);

        
    }

>>>>>>> master
}
