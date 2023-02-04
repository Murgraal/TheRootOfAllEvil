using System;
using UnityEngine;

public class CharacterSlider : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    private float _duration;

    public void Init(float sliderLength, float duration)
    {
        _startPos = transform.position;
        _endPos = transform.position + new Vector3(0, -sliderLength, 0);
        _duration = duration;
    }

    public float GetT(float startTime) => 1 - ((startTime + _duration - Time.time) / _duration);

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
