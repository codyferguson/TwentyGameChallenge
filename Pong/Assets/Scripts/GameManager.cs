using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Controllers {
    HUMAN,
    CPU
}

public class GameManager : MonoBehaviour {
    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    // For main scene
    public GameObject ball;
    public TMP_Text playerOneText;
    public TMP_Text playerTwoText;
    public GameObject endGameUI;
    public TMP_Text winnerText;

    private int _playerOneScore = 0;
    private int _playerTwoScore = 0;
    private BallController _ballController;

    //For menu scene
    public GameObject playerOne;
    public GameObject playerTwo;

    public Controllers playerOneControl;
    public Controllers playerTwoControl;
    private static int POINTS_FOR_WIN = 3;

    public void Awake() {
        //Check if instance already exists
        if (instance == null) {
            Debug.Log($"Instance is null.");
            instance = this;
        }
            

        else if (instance != this) {
            Debug.Log($"Destroying instance {instance}");
            Destroy(gameObject);
        }
            
        
        DontDestroyOnLoad(gameObject);
    }

    public void Start() {
        _playerOneScore = 0;
        _playerTwoScore = 0;
        ball = ball == null ? GameObject.Find("Ball") : ball;

        //Left here to play from Unity
        endGameUI = endGameUI ? endGameUI : GameObject.Find("EndGameText");
        if (endGameUI != null) endGameUI.SetActive(false);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    public void SetPlayerOneController(int controller) {
        playerOneControl = controller == ((int)Controllers.HUMAN) ? Controllers.HUMAN : Controllers.CPU;
        Debug.Log($"Setting player one  to {playerOneControl}");
    }

    public void SetPlayerTwoController(int controller) {
        playerTwoControl = controller == ((int)Controllers.HUMAN) ? Controllers.HUMAN : Controllers.CPU;
        Debug.Log($"Setting player two  to {playerTwoControl}");
    }

    // Triggered by event set in Unity UI
    public void PlayerOneScores() {
        _playerOneScore++;
        AfterScoring();
    }

    // Triggered by event set in Unity UI
    public void PlayerTwoScores() {
        _playerTwoScore++;
        AfterScoring();
    }
    
    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        Debug.Log($"scene loaded is {scene.name}");

        if (scene.name == "Main Scene") {
            ball = GameObject.Find("Ball");

            _ballController = ball.GetComponent<BallController>();
            // fetch text of UI - for some reason null by the time of scoring...
            playerOneText = GameObject.Find("PlayerOneScore").GetComponent<TMP_Text>();
            playerTwoText = GameObject.Find("PlayerTwoScore").GetComponent<TextMeshProUGUI>();
            _playerOneScore = 0;
            _playerTwoScore = 0;
            endGameUI = endGameUI ? endGameUI : GameObject.Find("EndGameText");
            if (endGameUI != null) endGameUI.SetActive(false);

            playerOne = GameObject.Find("Player 1");
            SetController(playerOne, playerOneControl);
            playerTwo = GameObject.Find("Player 2");
            SetController(playerTwo, playerTwoControl);
        }
    }

    private void AfterScoring() {
        // Check end game
        bool playerOneWins = _playerOneScore >= POINTS_FOR_WIN;
        bool playerTwoWins = _playerTwoScore >= POINTS_FOR_WIN;

        playerOneText = playerOneText ? playerOneText : GameObject.Find("PlayerOneScore").GetComponent<TMP_Text>();
        playerOneText.text = _playerOneScore.ToString();
        playerTwoText = playerTwoText ? playerTwoText : GameObject.Find("PlayerTwoScore").GetComponent<TMP_Text>();
        playerTwoText.text = _playerTwoScore.ToString();

        Debug.Log($"P1 score: {_playerOneScore} P2 score: {_playerTwoScore}");
        if (playerOneWins || playerTwoWins) {
            string displayWinner = playerOneWins ? "Player One" : "Player Two";
            StartCoroutine(EndGame(displayWinner));
        } else {
            // Why does OnLevelWasLoaded not instaniate it?
            _ballController = ball.GetComponent<BallController>();
            StartCoroutine(_ballController.ResetBall());
        }
    }

    private IEnumerator EndGame(string winner) {
        // display win
        ball.SetActive(false);
        endGameUI = endGameUI ? endGameUI : GameObject.Find("EndGameText");
        endGameUI.SetActive(true);
        winnerText = winnerText ? winnerText : GameObject.Find("Winning player").GetComponent<TMP_Text>();
        winnerText.text = $"{winner} wins!";
        _playerOneScore = 0;
        _playerTwoScore = 0;

        yield return new WaitForSeconds(1);

        // leave to main menu if it is previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void SetController(GameObject player, Controllers type) {
        Debug.Log($"Final game set up for {player.name} to enable controller {type}");
        player.GetComponent<PlayerController>().enabled = type == Controllers.HUMAN;
        player.GetComponent<ComputerController>().enabled = type == Controllers.CPU;
    }
}
