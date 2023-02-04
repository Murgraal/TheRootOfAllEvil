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
    
    
    public Level(LevelData data)
    {
        beatHits = new List<float>(data.BeatHit);
        track = data.Track;
    }
    
    public void ProgressLevel(float delta)
    {
        timer += delta;

        if (timer >= track.length)
        {
            StartLevel();
        }
        for (int i = beatsSpawned; i < beatHits.Count; i++)
        {
            if (timer > beatHits[i])
            {
                SpawnBeat?.Invoke();
            }
        }
    }

    public void StartLevel()
    {
        timer = 0;
        beatsSpawned = 0;
    }
}