using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public AudioSource source;
    public Slider slider;
    void Start()
    {
        slider.value = source.volume;
    }

    void Update()
    {
        source.volume = slider.value;
    }
}
