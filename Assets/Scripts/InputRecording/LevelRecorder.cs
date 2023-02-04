using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(AudioSource))]
public class LevelRecorder : MonoBehaviour
{
    [SerializeField] private LevelData currentRecorded;
    [SerializeField] private float timer;
    [SerializeField] private AudioSource audio;
    
    // Update is called once per frame
    void Update()
    {
        if (currentRecorded == null) return;
        timer += Time.deltaTime;
        if (timer >= currentRecorded.Track.length)
        {
            currentRecorded.StopRecording();
            audio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0f;
            currentRecorded.StartRecording();
            audio.clip = currentRecorded.Track;
            audio.loop = false;
            audio.time = 0f;
            audio.Play();
        }
            
        foreach (var key in Data.PolledKeycodes)
        {
            if (Input.GetKeyDown(key))
            {
                currentRecorded.RecordBeat(timer);
            }
        }
    }
}
