using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    public void PlayGame() {
        //SceneManager.LoadScene("Main");
        // gets the next scene in the queue. from build options
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
