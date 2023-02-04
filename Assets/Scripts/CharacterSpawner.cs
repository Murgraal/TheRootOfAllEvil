using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCharacter _movingCharacterPrefab;

    [SerializeField]
    private float _spawnTempo;
    private float _lastSpawnTime;
    private bool _isActive = false;

    public void StartSpawning()
    {
        _lastSpawnTime = Time.time;
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

        if (_lastSpawnTime + _spawnTempo < Time.time)
        {
            SpawnCharacter("");
        }
    }

    private void SpawnCharacter(string characterData)
    {
        var nextLetter = Main.GetLetterFromQueue();

        if (nextLetter == null)
        {
            _isActive = false;
            return;
        }

        var spawnedCharacter = GameObject.Instantiate(_movingCharacterPrefab);
        
        spawnedCharacter.Init(nextLetter);
        spawnedCharacter.data.startTime = Time.time;

        Debug.Log(spawnedCharacter.data.sliderIndex + " : " + spawnedCharacter.data.Letter);

        Main.GameManager.spawnedCharacters.Add(spawnedCharacter);
        _lastSpawnTime = Time.time;
    }
}

public enum SliderIndex
{
    Left = 0,
    Middle = 1,
    Right = 2,
    InvalidSlider = -1
}
