using UnityEngine;

public class CharacterSlider : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    private Vector2 _startHitZone;
    private Vector2 _endHitZone;

    public void Init(float sliderLength, float startHitZoneT, float hitzoneSize)
    {
        _startPos = transform.position;
        _endPos = transform.position + new Vector3(0, -sliderLength, 0);
        _startHitZone = GetPos(startHitZoneT);
        _endHitZone = GetPos(startHitZoneT + hitzoneSize);
    }

    public bool IsInHitZone(Vector2 position)
    {
        return position.y < _startHitZone.y && position.y > _endHitZone.y;
    }

    private float GetT(float startTime) => Time.time - startTime; // TODO: Make scalable time measurement

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