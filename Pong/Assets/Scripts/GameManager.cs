using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Controllers {
    HUMAN,
    CPU
}

public class GameManager : MonoBehaviour
{
    // For main scene
    public GameObject ball;
    public TMP_Text playerOneText;
    public TMP_Text playerTwoText;
    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    private int _playerOneScore;
    private int _playerTwoScore;
    private BallController _ballController;

    //For menu scene
    public TMP_Dropdown playerOneDropdown;
    public TMP_Dropdown playerTwoDropdown;

    private Controllers playerOneControl = Controllers.HUMAN;
    private Controllers playerTwoControl = Controllers.CPU;

    

    public void Awake() {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        // Kept here to run from scene
        _ballController = ball.GetComponent<BallController>();
        DontDestroyOnLoad(gameObject);
    }

    // Wired up to dropdown in menu. Triggers on value change
    public void SetPlayerOneController() {
        playerOneControl = playerOneDropdown.value == ((int)Controllers.HUMAN) ? Controllers.HUMAN : Controllers.CPU;
    }

    // Wired up to dropdown in menu. Triggers on value change
    public void SetPlayerTwoController() {
        playerTwoControl = playerTwoDropdown.value == ((int)Controllers.HUMAN) ? Controllers.HUMAN : Controllers.CPU;
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

    private void AfterScoring() {
        playerOneText.text = _playerOneScore.ToString();
        playerTwoText.text = _playerTwoScore.ToString();
        StartCoroutine(_ballController.ResetBall());
    }

    private void OnLevelWasLoaded(int level) {
        // don't check level. Just know this game only has one level
        ball = GameObject.Find("Ball"); // TODO: Change ball to GameObject
        _ballController = ball.GetComponent<BallController>();
    }
}
