using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private bool _isActive = false;

    public void StartSpawning()
    {
        Level.SpawnBeat += SpawnTick;
        _isActive = true;
    }

    public void StopSpawning()
    {
        _isActive = false;
    }

    public void SpawnTick()
    {
        if (!_isActive)
            return;

        SpawnCharacter("");

    }

    private void SpawnCharacter(string characterData)
    {
        var nextLetter = Main.GetLetterFromQueue();

        if (nextLetter == null)
        {
            _isActive = false;
            return;
        }

        var spawnedCharacter = Main.GameManager.Pool.Aquire();
        
        spawnedCharacter.Init(nextLetter);
        spawnedCharacter.data.startTime = Time.time;

        Debug.Log(spawnedCharacter.data.sliderIndex + " : " + spawnedCharacter.data.Letter);

        Main.GameManager.SpawnedCharacters.Add(spawnedCharacter);
    }
}

public class CharacterPool
{
    public MovingCharacter _prefab;
    public Stack<MovingCharacter> pooledCharacters = new Stack<MovingCharacter>();

    public CharacterPool(MovingCharacter prefab)
    {
        _prefab = prefab;
    }

    public MovingCharacter Aquire()
    {
        MovingCharacter aquiredCharacter = null;

        if (pooledCharacters.Count > 0)
            aquiredCharacter = pooledCharacters.Pop();
        else
            aquiredCharacter = GameObject.Instantiate(_prefab);

        aquiredCharacter.gameObject.SetActive(true);
        aquiredCharacter.IsActive = true;
        return aquiredCharacter;
    }

    public void Release(MovingCharacter character)
    {
        character.gameObject.SetActive(false);
        character.IsActive = false;
        pooledCharacters.Push(character);
    }
}

public enum SliderIndex
{
    Left = 0,
    Middle = 1,
    Right = 2,
    InvalidSlider = -1
}
