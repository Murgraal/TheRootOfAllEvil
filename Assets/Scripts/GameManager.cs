using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    public CharacterSpawner CharacterSpawner;
    public CharacterPool Pool;
    public List<MovingCharacter> SpawnedCharacters = new List<MovingCharacter>();

    public CharacterSliderSystem SliderSystem;

    private static Transform Line;

    [SerializeField]
    private MovingCharacter _movingCharacterPrefab;

    [SerializeField]
    private List<LevelData> LevelDatas;
    private Level CurrentLevel;

    private AudioSource audio;

    public static float LineYPos => Line == null ? 0 : Line.position.y;

    public float audioDelayTimer;
    public float audioStartDelayTime;

    private void Start()
    {
        CharacterSpawner.StartSpawning();
        SliderSystem.Init();
        Pool = new CharacterPool(_movingCharacterPrefab);
        audio = GetComponent<AudioSource>();
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        var levels = new List<LevelData>(LevelDatas);
        if (CurrentLevel != null)
        {
            levels.Remove(CurrentLevel.Data);
        }
        CurrentLevel = new Level(levels[Random.Range(0, levels.Count)]);
        audio.loop = true;
        audio.clip = CurrentLevel.Data.Track;
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
        UpdateCharacters();
    }

    private void UpdateCharacters()
    {
        foreach (var character in SpawnedCharacters)
        {
            if (character.IsActive)
            {
                character.UpdatePosition();
            }
        }
    }
}