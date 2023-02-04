using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<KeyCode> OnKeyPressed;
    public CharacterSpawner CharacterSpawner;
    public List<MovingCharacter> spawnedCharacters;
    private void Start()
    {
        CharacterSpawner.StartSpawning();
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