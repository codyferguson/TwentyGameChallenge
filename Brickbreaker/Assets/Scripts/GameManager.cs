using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject mainMennu;
    [SerializeField] GameObject gameHud;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject bricksPrefab;
    [SerializeField] GameObject bricks;
    [SerializeField] GameObject paddle;

    public Slider volumeSlider;

    int totalBrickCount = 0;
    private int lifes = 3;
    private PaddleController paddleController;
    bool isPlaying = false;
    SoundManager soundManager;
    
    void Start()
    {
        totalBrickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
        bricks = GameObject.Find("Bricks").gameObject;
        paddle = GameObject.Find("Paddle");
        paddleController = paddle.GetComponent<PaddleController>();

        // All menus needed to navigate UI
        GameObject canvas = GameObject.Find("Canvas");
        pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
        gameOverMenu = canvas.transform.Find("Game Over Menu").gameObject;
        gameHud = canvas.transform.Find("Game Hud").gameObject;
        mainMennu = canvas.transform.Find("Main Menu").gameObject;
        optionsMenu = canvas.transform.Find("Options Menu").gameObject;

        isPlaying = false;
        soundManager = SoundManager.instance;
    }

    void Update()
    {
        if (isPlaying && Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }

        if (GameObject.FindGameObjectsWithTag("Brick").Length <= 0 || lifes <= 0) {
            EndGameSecquence();
        }
    }

    public void PlayGame() {
        Debug.Log("Starting game...");
        gameHud.SetActive(true);
        mainMennu.SetActive(false);
        InitializeGame();
    }

    public void ExitToMainMenu() {
        //reset board
        bricks = Instantiate(bricksPrefab, bricks.transform);
        gameHud.SetActive(false);
        pauseMenu.SetActive(false);
        mainMennu.SetActive(true);
    }

    public void OpenOptionsWindow() {
        mainMennu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OptionsBackButton() {
        mainMennu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ExitGame() {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    public void ReplayGame() {
        gameHud.SetActive(true);
        gameOverMenu.SetActive(false);
        InitializeGame();
        Debug.Log("Reset board and lives");
    }

    public void UnPauseGame() {
        isPlaying = true;
        pauseMenu.SetActive(false);
        Ball ball = GameObject.Find("Ball(Clone)").GetComponent<Ball>();
        ball.UnpauseBall();
    }

    public void AdjustGameVolume() {
        // TODO: get sound manager instance
        soundManager.AdjustVolume(Mathf.Log10(volumeSlider.value) * 20);
    }

    private void EndGameSecquence() {
        gameOverMenu.transform.Find("Game over title").GetComponent<TextMeshPro>().text = "You win!";
        gameOverMenu.SetActive(true);
        isPlaying = false;
        // pause game
    }

    private void PauseGame() {
        isPlaying = false;
        pauseMenu.SetActive(true);
        Ball ball = GameObject.Find("Ball(Clone)").GetComponent<Ball>();
        ball.PauseBall(); 
    }

    private void InitializeGame() {
        isPlaying = true;
        paddleController.TogglePaddlePause(false);
        // set score to 00
        // set lives to 3
        // position paddle to middle spot
        // delete ball if present and reset bricks
    }
}
