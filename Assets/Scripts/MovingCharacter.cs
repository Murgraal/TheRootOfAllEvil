using System;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public MovingCharacterData data;
    public static event Action<MovingCharacterData> OnEnterHitZone;
    public static event Action<MovingCharacterData> OnExitHitZone;

    private bool _isInHitZone;

    public void Init(MovingCharacterData data)
    {
        this.data = data;
        _isInHitZone = false;
    }

    public void UpdatePosition()
    {
        var slider = Main.GameManager.CharacterSpawner.GetSliderByIndex(data.sliderIndex);

        transform.position = slider.GetPosByTime(data.startTime);

        if (slider.IsInHitZone(transform.position) && !_isInHitZone)
            EnterHitZone();
        else if (!slider.IsInHitZone(transform.position) && _isInHitZone)
            ExitHitZone();
    }

    public void EnterHitZone()
    {
        _isInHitZone = true;
        OnEnterHitZone?.Invoke(data);
    }

    public void ExitHitZone()
    {
        _isInHitZone = false;
        OnExitHitZone?.Invoke(data);
    }
}