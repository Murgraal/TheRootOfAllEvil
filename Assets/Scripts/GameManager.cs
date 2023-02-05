using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    public CharacterSpawner CharacterSpawner;
    

    public CharacterSliderSystem SliderSystem;

    public EffectSpawner EffectSpawner;

    private static Transform Line;

    [SerializeField]
    private MovingCharacter _movingCharacterPrefab;

    [SerializeField]
    private List<LevelData> LevelDatas;
    private Level CurrentLevel;

    public AudioSource audio;
    public SfxMapping[] sfxClips;
    public AudioClip missClip;

    public AudioSource[] audiosources;
    public AudioSource sfxSource;

    public static float LineYPos => Line == null ? 0 : Line.position.y;

    public float audioDelayTimer;
    public float audioStartDelayTime;

    private void OnEnable()
    {
        CharacterSpawner.CharacterSpawned += AddSpawnedCharacter;
        Main.OnSucessfulHit += PlaySfx;
        Main.OnSucessfulHit += EffectSpawner.SpawnHitEffect;
    }

    private void OnDisable()
    {
        CharacterSpawner.CharacterSpawned -= AddSpawnedCharacter;
        Main.OnSucessfulHit -= PlaySfx;
        Main.OnSucessfulHit -= EffectSpawner.SpawnHitEffect;
    }

    public void AddSpawnedCharacter(MovingCharacter spawnedCharater)
    {
        Data.GamePlay.SpawnedCharacters.Add(spawnedCharater);
    }

    public void PlayMissSfx()
    {
        sfxSource.PlayOneShot(missClip);
    }
    
    private void PlaySfx(Vector3 pos)
    {
        var sfx = sfxClips.FirstOrDefault(x => x.multiplierLevel == Data.GamePlay.MultiplierLevel);
        if (sfx != null)
        {
            sfxSource.PlayOneShot(sfx.clip);  
        }
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

    [Serializable]
    public class SfxMapping
    {
        public Data.MultiplierLevel multiplierLevel;
        public AudioClip clip;
    }
}