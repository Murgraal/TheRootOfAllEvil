using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    public CharacterSpawner CharacterSpawner;

    public List<MovingCharacter> spawnedCharacters;

    public CharacterSliderSystem SliderSystem;

    private static Transform Line;

    private CharacterSliderSystem _sliderSystem;

    public static float LineYPos => Line == null ? 0 : Line.position.y;

    private void Start()
    {
        CharacterSpawner.StartSpawning();
        SliderSystem.Init();
    }

    private void Update()
    {
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