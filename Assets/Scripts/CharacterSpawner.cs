using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCharacter _keyPrefab;

    private bool _isActive = false;

    public void StartSpawning()
    {
        Level.SpawnBeat += SpawnCharacter;
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

        Main.GameManager.SpawnedCharacters.Add(spawnedCharacter);
    }

    public void DespawnCharacter(MovingCharacter character, float delay = 0)
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
