using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class BouncySurface : MonoBehaviour
{
    public float bounceStrength;
    public AudioClip hitSound;
    public TMP_Text scoreText;

    UnityEvent hitEvent;

    public void Start() {
        //SoundManager soundManager = SoundManager.instance;
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();

        if (hitEvent == null) {
            hitEvent = new UnityEvent();
        }

        // TODO: Add different hit sounds to different tags
        //hitEvent.AddListener(() => soundManager.PlaySingle(hitSound));
        if (tag == "Brick") hitEvent.AddListener(() => UpdateScore());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var xCor = collision.GetContact(0).point.x;
        var yCor = collision.GetContact(0).point.y;
        
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null) {
            switch (tag) {
                case "Brick":
                    HandleBallNewForce(ball, -collision.GetContact(0).normal);
                    Destroy(this.gameObject);
                    break;
                case "Player":
                    PaddleController paddle = GetComponent<PaddleController>();
                    HandleBallNewForce(ball, paddle.DeterminePaddleRegion(xCor));
                    break;
                case "Boundary":
                    if (name == "BoundaryBottom") {
                        StartCoroutine(ball.ResetBall());
                        HandleLostBallEvent(ball);
                    }
                    HandleBallNewForce(ball, -collision.GetContact(0).normal);
                    break;
                default:
                    Debug.Log($"object not supported with tag {tag}");
                    break;
            }
        }
    }

    /// <summary>
    /// Old code that was trying to local cell in tile map from grid. It works for a normal tilemap but
    /// I had to change the size to make the bricks look like long rectangles which threw off finding the tile cell.
    /// </summary>
    /// <param name="xCor"></param>
    /// <param name="yCor"></param>
    private void FindTileToRemove(float xCor, float yCor) {
        Tilemap bricks = GetComponent<Tilemap>();

        Vector3Int floorTilePos = Vector3Int.FloorToInt(new Vector3(xCor, yCor));
        
        Vector3Int ceilTilePos = Vector3Int.FloorToInt(new Vector3(xCor, yCor));
        Vector3Int roundTilePos = Vector3Int.RoundToInt(new Vector3(xCor, yCor));
        Vector3Int tilemapPosition = bricks.layoutGrid.WorldToCell(new Vector3(xCor, yCor));
        Vector3Int otherTilemapPos = bricks.layoutGrid.LocalToCell(new Vector3(xCor, yCor));

        if (bricks.GetTile(tilemapPosition)) {
            bricks.SetTile(tilemapPosition, null);
            Debug.Log($"Tile removed from World to Cell tile position {tilemapPosition}");
        } else if (bricks.GetTile(otherTilemapPos)) {
            bricks.SetTile(tilemapPosition, null);
            Debug.Log($"Tile removed from World to Cell tile position {otherTilemapPos}");
        } else if (bricks.GetTile(roundTilePos)) {
            bricks.SetTile(tilemapPosition, null);
            Debug.Log($"Tile removed from round tile position {roundTilePos}");
        } else if (bricks.GetTile(ceilTilePos)) {
            bricks.SetTile(tilemapPosition, null);
            Debug.Log($"Tile removed from ceil tile position {ceilTilePos}");
        } else {
            bricks.SetTile(tilemapPosition, null);
            Debug.Log($"Tile removed from default floor tile position {floorTilePos}");
        }

        // TODO: Add points to player and speed up ball
    }

    private void HandleLostBallEvent(Ball ball) {
        string nameToRemove = $"BallLife{ball.DecrementLife()}";

        if (nameToRemove.Equals("BallLife0")) {
            Debug.Log($"Would reset game");
            return;
        }

        GameObject lifeToRemove = GameObject.Find(nameToRemove);

        if (lifeToRemove != null) {
            lifeToRemove.SetActive(false);
        }
    }

    private void HandleBallNewForce(Ball ball, Vector2 direction) {
        //Debug.Log($"send ball {direction}");
        ball.AddForce(direction * bounceStrength);
        hitEvent.Invoke();
    }

    private void UpdateScore() {
        int newScore = int.Parse(scoreText.text) + 100;
        scoreText.text = newScore.ToString();
    }
}
