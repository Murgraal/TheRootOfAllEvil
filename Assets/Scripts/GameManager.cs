using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    public CharacterSpawner CharacterSpawner;

    public List<MovingCharacter> spawnedCharacters;

    public CharacterSliderSystem SliderSystem;

    private static Transform Line;

    [SerializeField]
    private List<LevelData> LevelDatas;
    private Level CurrentLevel;

    private CharacterSliderSystem _sliderSystem;

    public static float LineYPos => Line == null ? 0 : Line.position.y;

    private void Start()
    {
        CharacterSpawner.StartSpawning();
        SliderSystem.Init();
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
        
    }

    private void Update()
    {
        if (CurrentLevel == null) return; 
        
        CurrentLevel.ProgressLevel(Time.deltaTime);
        foreach (var key in Data.PolledKeycodes)
        {
            if (Input.GetKeyDown(key))
            {
                OnKeyPressed?.Invoke(key);
            }
        }
        CharacterSpawner.SpawnTick();
        UpdateCharacters();
    }

    private void UpdateCharacters()
    {
        foreach (var character in spawnedCharacters)
        {
            character.UpdatePosition();
        }
    }
}