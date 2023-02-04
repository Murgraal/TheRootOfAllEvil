using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelBeatData", fileName = "LevelBeatData")]
public class LevelData : ScriptableObject
{
    public List<float> BeatHit = new List<float>();
    public AudioClip Track;
    
    public bool canRecord = true;
    
    public void StartRecording()
    {
        if(canRecord)
            BeatHit.Clear();
    }

    public void StopRecording()
    {
        canRecord = false;
    }

    public void RecordBeat(float time)
    {
        if(canRecord)
            BeatHit.Add(time);
    }
}