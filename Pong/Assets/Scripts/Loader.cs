using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject soundManager;

    // Start is called before the first frame update
    public void Awake() {
        if (GameManager.instance == null) {
            Debug.Log($"Game manager instance was null");
            Instantiate(gameManager);
        }
            
        if (SoundManager.instance == null) {
            Debug.Log($"Game manager instance was null");
            Instantiate(soundManager);
        }
            
    }
}
