using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private MovingCharacter _movingCharacterPrefab;
    private float _spawnTempo;
    private float _startTime;
    private bool _isActive = false;

    CharacterSlider[] _characterSliders = new CharacterSlider[3];

    private void StartSpawning()
    {
        _startTime = Time.time;
        _isActive = true;
    }

    public void SpawnTick()
    {
        if (!_isActive)
            return;

        if (_startTime % _spawnTempo == 0)
        {
            SpawnCharacter("");
        }
    }

    private void SpawnCharacter(string characterData)
    {
        var spawnedCharacter = GameObject.Instantiate(_movingCharacterPrefab);
        var nextLetter = Main.GetLetterFromQueue();

        if (nextLetter == null)
            return;
        
        spawnedCharacter.Init(nextLetter);
    }
}
