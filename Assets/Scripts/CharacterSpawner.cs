using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCharacter _movingCharacterPrefab;

    [SerializeField]
    private CharacterSlider[] _characterSliders = new CharacterSlider[3];

    private float _spawnTempo = 2f;
    private float _lastSpawnTime;
    private bool _isActive = false;

    public CharacterSlider GetSliderByIndex(SliderIndex index) => _characterSliders[(int)index];

    public void StartSpawning()
    {
        _lastSpawnTime = Time.time;
        _isActive = true;

        _characterSliders[0].Init(5f, 0.8f, 0.9f);
        _characterSliders[1].Init(5f, 0.8f, 0.9f);
        _characterSliders[2].Init(5f, 0.8f, 0.9f);
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
