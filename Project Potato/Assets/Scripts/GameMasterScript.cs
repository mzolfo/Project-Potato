using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterScript : MonoBehaviour
{
    public static GameMasterScript _instance;

    [SerializeField]
    private Transform bombRespawnPos;
    [SerializeField]
    private Transform player1RespawnPos;
    [SerializeField]
    private Transform player2RespawnPos;

    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private GameObject player1Prefab;
    [SerializeField]
    private GameObject player2Prefab;

    [SerializeField]
    private Text timerTextBox;
    [SerializeField]
    private Text restartTextBox;

    private GameObject currentBomb;
    private GameObject currentPlayer1;
    private GameObject currentPlayer2;
    private BombStatusScript currentBombScript;

    [SerializeField]
    private float bombTimerStart;
    private float bombTimer;
    public bool timerOn = false;

    public static bool gameOver = false;

    //private bool canResetGame = false;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        

        restartTextBox.enabled = false;

        bombTimer = bombTimerStart;
        timerTextBox.text = null;
        ResetGame();
        currentBomb = GameObject.FindGameObjectWithTag("Bomb");
        currentPlayer1 = GameObject.FindGameObjectWithTag("Player 1");
        currentPlayer2 = GameObject.FindGameObjectWithTag("Player 2");
        currentBombScript = currentBomb.GetComponent<BombStatusScript>();

        if (currentPlayer1 == null || currentPlayer2 == null || currentBomb == null)
            Debug.LogError("Something wasnt found");
    }

    void Update()
    {

        if (timerOn && bombTimer > 0 && currentBombScript.isThrown == true)
        {
            bombTimer -= Time.deltaTime / 2;
            timerTextBox.text = bombTimer.ToString("F2");
            Debug.Log("Timer is slow?");
        }
        else if (timerOn && bombTimer > 0)
        {
            bombTimer -= Time.deltaTime;
            timerTextBox.text = bombTimer.ToString("F2");
        }
        else if (currentBombScript.isThrown == false && bombTimer <= 0)
        {
            timerOn = false;
            currentBombScript.BombExplodes();
            bombTimer = bombTimerStart;
            timerTextBox.text = "0.00";
        }


        if (gameOver)
        {
            int count = 0;
            if (count == 0)
            {
                GameOver();
                count++;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetGame();
            }
        }
        

    }

    private void GameOver()
    {
        restartTextBox.enabled = true;
        timerTextBox.text = "KABOOM";
        restartTextBox.text = "To restart hit the R key";
        //gameOver = false;
        //canResetGame = true;
    }

    private void ResetGame()
    {
        if (currentBomb != null)
            Debug.LogError("Game should not have ended, error cannot compute, fix it");

        GameObject theBomb = Instantiate(bombPrefab, bombRespawnPos);
        theBomb.transform.position = bombRespawnPos.position;
        theBomb.name = "TheBomb";
        currentBomb = theBomb;
        currentBombScript = currentBomb.GetComponent<BombStatusScript>();


        if (currentPlayer1 == null && currentPlayer2 == null)
        {
            SpawnPlayer1();
            SpawnPlayer2();
        }
        else if (currentPlayer1 == null)
        {
            SpawnPlayer1();
        }
        else if (currentPlayer2 == null)
        {
            SpawnPlayer2();
        }

        timerTextBox.text = null;
        restartTextBox.enabled = false;
        gameOver = false;
        //canResetGame = false;
    }

    private void SpawnPlayer1()
    {
        GameObject newPlayer1 = Instantiate(player1Prefab, player1RespawnPos);
        newPlayer1.name = "Player1";
        newPlayer1.transform.parent = null;
        currentPlayer1 = newPlayer1;

        if (currentPlayer2 != null)
            currentPlayer2.transform.position = player2RespawnPos.position;

    }
    private void SpawnPlayer2()
    {
        GameObject newPlayer2 = Instantiate(player2Prefab, player2RespawnPos);
        newPlayer2.name = "Player2";
        newPlayer2.transform.parent = null;
        currentPlayer2 = newPlayer2;

        if (currentPlayer1 != null)
            currentPlayer1.transform.position = player1RespawnPos.position;
    }


    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
