using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    public CharacterSpawner CharacterSpawner;
    public List<MovingCharacter> SpawnedCharacters = new List<MovingCharacter>();

    public CharacterSliderSystem SliderSystem;

    public EffectSpawner EffectSpawner;

    private static Transform Line;

    [SerializeField]
    private MovingCharacter _movingCharacterPrefab;

    [SerializeField]
    private List<LevelData> LevelDatas;
    private Level CurrentLevel;

    public AudioSource audio;

    public AudioSource[] audiosources;

    public static float LineYPos => Line == null ? 0 : Line.position.y;

    public float audioDelayTimer;
    public float audioStartDelayTime;

    private void Start()
    {
        CharacterSpawner.StartSpawning();
        SliderSystem.Init();
    }

    public void StartNewLevel()
    {
        var levels = new List<LevelData>(LevelDatas);
        if (CurrentLevel != null)
        {
            levels.Remove(CurrentLevel.Data);
        }
        var random = Random.Range(0, levels.Count);
        CurrentLevel = new Level(levels[random]);

        audio = audiosources[random];
    }

    private void Update()
    {
        if (CurrentLevel == null) return;

        audioDelayTimer += Time.deltaTime;
        if (audioDelayTimer >= audioStartDelayTime && !audio.isPlaying)
        {
            audio.Play();
        }
        
        CurrentLevel.ProgressLevel(Time.deltaTime);
        foreach (var key in Data.PolledKeycodes)
        {
            if (Input.GetKeyDown(key))
            {
                OnKeyPressed?.Invoke(key);
            }
        }
    }
}