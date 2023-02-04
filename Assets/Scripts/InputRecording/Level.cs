using System;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private float timer;
    private int beatsSpawned;
    public static event Action SpawnBeat;
    private List<float> beatHits;
    private AudioClip track;

    public LevelData Data { get; private set; }
    
    public Level(LevelData data)
    {
        beatHits = new List<float>(data.BeatHit);
        track = data.Track;
        Data = data;
    }
    
    public void ProgressLevel(float delta)
    {
        timer += delta;

        if (timer >= track.length)
        {
            ResetSession();
        }
        for (int i = beatsSpawned; i < beatHits.Count; i++)
        {
            if (timer > beatHits[i])
            {
                SpawnBeat?.Invoke();
                beatsSpawned++;
            }
        }
    }

    public void ResetSession()
    {
        timer = 0;
        beatsSpawned = 0;
    }
}