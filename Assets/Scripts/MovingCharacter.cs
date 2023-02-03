using System;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public MovingCharacterData data;
    public static event Action<MovingCharacterData> OnEnterHitZone;
    public static event Action<MovingCharacterData> OnExitHitZone;
    public void Init(MovingCharacterData data)
    {
        this.data = data;
    }

    public void UpdatePosition()
    {
        var slider = Main.GameManager.GetCharacterSlider(data.sliderIndex);

        transform.position = slider.GetPosByTime(data.startTime);

        if (slider.IsInHitZone(transform.position))
        {
            EnterHitZone();
        }
        if (slider.HasReachedEnd(data.startTime))
        {

        }
    }

    public void EnterHitZone()
    {
        OnEnterHitZone?.Invoke(data);
    }

    public void ExitHitZone()
    {
        OnExitHitZone?.Invoke(data);
        
    }
}