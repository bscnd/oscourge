using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour {

    public AudioMixer mixer;
    public string name;
    public Slider slider;

        void Start()
    {
        slider.value = PlayerPrefs.GetFloat(name, 0.75f);
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat(name, Mathf.Log10(sliderValue) * 20);
          PlayerPrefs.SetFloat(name, sliderValue);
    }
}