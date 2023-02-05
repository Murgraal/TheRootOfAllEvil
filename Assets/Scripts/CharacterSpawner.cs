using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCharacter _keyPrefab;

    private bool _isActive = false;

    public static event Action<MovingCharacter> CharacterSpawned;

    private void OnEnable()
    {
        Level.SpawnBeat += SpawnCharacter;
    }

    private void OnDisable()
    {
        Level.SpawnBeat -= SpawnCharacter;
    }

    public void StartSpawning()
    {
        
        _isActive = true;
    }

    public void StopSpawning()
    {
        _isActive = false;
    }
    
    private void SpawnCharacter()
    {
        if (!_isActive)
            return;
        
        var nextLetter = Main.GetLetterFromQueue();

        if (nextLetter == null)
        {
            _isActive = false;
            return;
        }

        var spawnedCharacter = GameObject.Instantiate(_keyPrefab); // Get character object
        
        spawnedCharacter.Init(nextLetter);

//        Debug.Log(spawnedCharacter.data.sliderIndex + " : " + spawnedCharacter.data.Letter);

        CharacterSpawned?.Invoke(spawnedCharacter);
    }

    public static void DespawnCharacter(MovingCharacter character, float delay = 0)
    {
        Destroy(character.gameObject, delay);
    }

}

public enum SliderIndex
{
    Left = 0,
    Middle = 1,
    Right = 2,
    InvalidSlider = -1
}
