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

    private void OnEnable()
    {
        CharacterSpawner.CharacterSpawned += AddSpawnedCharacter;
    }

    private void OnDisable()
    {
        CharacterSpawner.CharacterSpawned -= AddSpawnedCharacter;
    }

    public void AddSpawnedCharacter(MovingCharacter spawnedCharater)
    {
        SpawnedCharacters.Add(spawnedCharater);
    }

    private void Start()
    {
        CharacterSpawner.StartSpawning();
        SliderSystem.Init();
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        CurrentLevel = new Level(LevelDatas[Data.GamePlay.Level]);
        audio = audiosources[Data.GamePlay.Level];
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