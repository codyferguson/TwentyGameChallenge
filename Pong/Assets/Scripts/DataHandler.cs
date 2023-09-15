using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * This class was only created to remove headache with game manger going to DontDestroyOnLoad which breaks listeners on drop downs
 * There has to be a better way to do this, but I need to move on and get this working
 */
public class DataHandler : MonoBehaviour
{
    public TMP_Dropdown playerOneDropdown;
    public TMP_Dropdown playerTwoDropdown;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null) {
            Debug.Log($"Game manager is null try {GameManager.instance}.");
            gameManager = GameManager.instance;
        }
    }

    // Wired up to dropdown in menu. Triggers on value change
    public void DropdownListener(string player) {
        if (player == "player one") {
            Debug.Log($"Player one is {playerOneDropdown.value}");
            gameManager.SetPlayerOneController(playerOneDropdown.value);
        } else {
            Debug.Log($"Player two is {playerTwoDropdown.value}");
            gameManager.SetPlayerTwoController(playerTwoDropdown.value);
        }
    }
}
