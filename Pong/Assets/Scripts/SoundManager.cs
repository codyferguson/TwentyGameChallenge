using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource gameBackgroundSource;
    public static SoundManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    // Start is called before the first frame update
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip) {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSound(string sourceType, params AudioClip[] clips) {
        switch (sourceType) {
            case "efx":
                RandomizeSfx(clips);
                break;
            case "ambient":
                PlayAmbientSound(clips);
                break;
            default:
                break;
        }
    }

    private void RandomizeSfx(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    private void PlayAmbientSound(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        gameBackgroundSource.pitch = randomPitch;
        gameBackgroundSource.clip = clips[randomIndex];
        gameBackgroundSource.Play();
    }
}
