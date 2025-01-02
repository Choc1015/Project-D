using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;

    public void PlayOneShotSound(SoundData data)
    {
        audioSource.PlayOneShot(data.clip);
    }
}

[System.Serializable]
public class SoundData
{
    public string name;
    public AudioClip clip;
}
