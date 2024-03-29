using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource gameBackgroundSource;
    public static SoundManager instance = null;
    public AudioMixer mainMixer;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AdjustVolume(float sliderValue) {
        mainMixer.SetFloat("MasterVolume", sliderValue);
    }

    /// <summary>
    /// This method gets referenced from various scripts by adding event listeners that when invoked, call this method
    /// </summary>
    /// <param name="clip"></param>
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
