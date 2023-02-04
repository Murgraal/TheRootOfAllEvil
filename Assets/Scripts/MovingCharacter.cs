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
        var sliderSystem = Main.GameManager.SliderSystem;
        var slider = sliderSystem.GetSliderByIndex(data.sliderIndex);
        var distanceTraveled = slider.GetT(data.startTime);

        transform.position = slider.GetPosByTime(data.startTime);
        data.yPos = transform.position.y;

        if (sliderSystem.IsInHitZone(distanceTraveled) && !_isInHitZone)
            EnterHitZone();
        else if (!sliderSystem.IsInHitZone(distanceTraveled) && _isInHitZone)
            ExitHitZone();
        if (slider.HasReachedEnd(distanceTraveled))
        {
            Debug.Log("Reached End");
        }
    }

    public void EnterHitZone()
    {
        Debug.Log("In hit zone");
        _isInHitZone = true;
        OnEnterHitZone?.Invoke(data);
    }

    public void ExitHitZone()
    {
        _isInHitZone = false;
        OnExitHitZone?.Invoke(data);
    }
}