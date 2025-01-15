using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public string objName;
    public AudioSource source;
    public Slider slider;
    void Start()
    {
        source = SoundManager.Instance.GetSource(objName);
        slider.value = source.volume;
    }


    public void ChangeVolume() => source.volume = slider.value;
}
