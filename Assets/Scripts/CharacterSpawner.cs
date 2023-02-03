using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private GameObject _movingCharacterPrefab;
    private float _spawnTempo;
    private float _startTime;
    private bool _isActive;

    CharacterSlider[] _characterSpawnPoints = new CharacterSlider[3];

    private void StartSpawning()
    {
        _startTime = Time.time;
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
        GameObject.Instantiate(_movingCharacterPrefab);
    }

    private int GetCharacterSpawnPointIndex(KeyCode key)
    {
        for (int i = 0; i < Data.KeyBoardLayout.Count; i++)
        {
            if (Data.KeyBoardLayout[i].ContainsKey(key))
            {
                // TODO check if slider exists
                return i;
            }
        }
        Debug.Log($"Key {key} is not a letter");
        return -1;
    }
}

public class CharacterSlider : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    private Vector2 _startHitZone;
    private Vector2 _endHitZone;

    public void Init(Vector2 startPos, Vector2 endPos, float startHitZoneT, float hitzoneSize)
    {
        _startPos = startPos;
        _endPos = endPos;
        _startHitZone = GetPos(startHitZoneT);
        _endHitZone = GetPos(startHitZoneT + hitzoneSize);
    }

    public bool IsInHitZone(Vector2 position)
    {
        return position.y < _startHitZone.y && position.y > _endHitZone.y;
    }

    private float GetT(float startTime) => startTime - Time.time;

    public bool HasReachedEnd(float startTime)
    {
        return GetT(startTime) >= 1;
    }

    public Vector2 GetPosByTime(float startTime)
    {
        return GetPos(GetT(startTime));
    }

    public Vector2 GetPos(float t)
    {
        return Vector2.Lerp(_startPos, _endPos, t);
    }
}