using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgmSource, effectSource;
    [SerializeField] private SoundData dataTemp;
    private float timer;
    public void PlayOneShotSound(SoundData data, bool isBgmStop = false)
    {
        if (dataTemp != null && dataTemp.name == data.name)
            return;
        effectSource.PlayOneShot(data.clip);
        timer = 0.05f;
        dataTemp = data;
        if(isBgmStop)
            bgmSource.Stop();
    }
    public void PlayLoopSound(SoundData data)
    {
        bgmSource.Stop();
        bgmSource.clip = data.clip;
        bgmSource.Play();

    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            dataTemp = null;
    }
}

[System.Serializable]
public class SoundData
{
    public string name;
    public AudioClip clip;
}
