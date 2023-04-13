using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BallController ball;

    private int _playerOneScore;
    private int _playerTwoScore;

    public void PlayerOneScores() {
        _playerOneScore++;

        ball.ResetBall();
    }

    public void PlayerTwoScores() {
        _playerTwoScore++;

        ball.ResetBall();
    }
}
