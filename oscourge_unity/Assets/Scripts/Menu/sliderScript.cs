using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour {

    public AudioMixer mixer;
    public string namee;
    public Slider slider;

        void Start()
    {
        slider.value = PlayerPrefs.GetFloat(namee, 0.75f);
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat(namee, Mathf.Log10(sliderValue) * 20);
          PlayerPrefs.SetFloat(namee, sliderValue);
    }
}