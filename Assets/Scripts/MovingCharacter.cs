using System;
using UnityEngine;
using TMPro;

public class MovingCharacter : MonoBehaviour
{
    public MovingCharacterData data;
    public static event Action<MovingCharacterData> OnEnterHitZone;
    public static event Action<MovingCharacterData> OnExitHitZone;

    public bool IsActive;
    private bool _isInHitZone;
    [SerializeField]
    private TMP_Text keyText;

    public void Init(MovingCharacterData data)
    {
        this.data = data;
        _isInHitZone = false;
        keyText.text = this.data.Letter.ToString();
    }

    public void UpdatePosition()
    {
        if (!IsActive) return;

        var sliderSystem = Main.GameManager.SliderSystem;
        var slider = sliderSystem.GetSliderByIndex(data.sliderIndex);
        var distanceTraveled = slider.GetT(data.startTime);

        transform.position = slider.GetPosByTime(data.startTime);
        data.yPos = transform.position.y;

        if (sliderSystem.IsInHitZone(distanceTraveled) && !_isInHitZone)
            EnterHitZone();
        else if (!sliderSystem.IsInHitZone(distanceTraveled) && _isInHitZone)
            ExitHitZone();
        if (distanceTraveled >= 1)
        {
            Main.GameManager.Pool.Release(this);
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