using System;
using UnityEngine;
using TMPro;

public class MovingCharacter : MonoBehaviour
{
    public MovingCharacterData data;
    public static event Action<MovingCharacterData> OnEnterHitZone;
    public static event Action<MovingCharacterData> OnExitHitZone;

    private bool _isInHitZone;
    [SerializeField]
    private TMP_Text keyText;

    private CharacterSliderSystem sliderSystem;
    private CharacterSpawner characterSpawner;

    private void Start()
    {
        sliderSystem = FindObjectOfType<CharacterSliderSystem>();
        characterSpawner = FindObjectOfType<CharacterSpawner>();
    }

    public void Init(MovingCharacterData data)
    {
        this.data = data;
        data.startTime = Time.time;
        _isInHitZone = false;
        keyText.text = this.data.Letter.ToString();
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
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
            Main.ReduceHealth();
            Main.EndStreak(transform.position);
            Main.GameManager.EffectSpawner.SpawnMissEffect(transform.position);
            Main.GameManager.PlayMissSfx();
            CharacterSpawner.DespawnCharacter(this,0f);
        }
    }

    public void EnterHitZone()
    {
        //Debug.Log("In hit zone");
        _isInHitZone = true;
        OnEnterHitZone?.Invoke(data);
    }

    public void ExitHitZone()
    {
        _isInHitZone = false;
        OnExitHitZone?.Invoke(data);
    }
}