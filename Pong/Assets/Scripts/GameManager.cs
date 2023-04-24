using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BallController ball;
    public TMP_Text playerOneText;
    public TMP_Text playerTwoText;

    private int _playerOneScore;
    private int _playerTwoScore;

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
