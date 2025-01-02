using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundController : MonoBehaviour
{
    public SoundData[] soundDatas;
    public void PlayOneShotSound(string soundName)
    {
        IEnumerable<SoundData> curDatas = soundDatas.Where(x => x.name == soundName);
        foreach(SoundData curData in curDatas)
        {
            //SoundManager.Instance.PlayOneShotSound(curData);
            Debug.Log($"{curData.name} Play !");
        }
    }
}
