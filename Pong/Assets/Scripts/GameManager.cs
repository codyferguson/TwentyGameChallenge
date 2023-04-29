using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BallController ball;
    public TMP_Text playerOneText;
    public TMP_Text playerTwoText;
    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    private int _playerOneScore;
    private int _playerTwoScore;

    public void Awake() {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayerOneScores() {
        _playerOneScore++;
        playerOneText.text = _playerOneScore.ToString();
        ball.ResetBall();
    }

    public void PlayerTwoScores() {
        _playerTwoScore++;
        playerTwoText.text = _playerTwoScore.ToString();
        ball.ResetBall();
    }
}
