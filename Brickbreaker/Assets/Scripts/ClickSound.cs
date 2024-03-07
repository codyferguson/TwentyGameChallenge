using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickSound : MonoBehaviour {
    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    void Start() {
        SoundManager soundManager = SoundManager.instance;
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        // Any other settings you want to initialize

        button.onClick.AddListener(() => soundManager.PlaySingle(sound));
    }
}
