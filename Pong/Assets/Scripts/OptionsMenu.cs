using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundManager.instance;
        volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();
    }

    // This gets call from listener added in UI
    public void AdjustVolume() {
        soundManager.AdjustVolume(Mathf.Log10(volumeSlider.value) * 20);
    }
}
