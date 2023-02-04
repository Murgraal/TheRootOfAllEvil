using UnityEngine;

public class CharacterSliderSystem : MonoBehaviour
{
    [SerializeField]
    private float _hitZoneStart;

    [SerializeField]
    private float _hitZoneEnd;

    [SerializeField]
    private float noteDuration;

    [SerializeField]
    private CharacterSlider[] _characterSliders;

    public void Init()
    {
        _characterSliders[0].Init(5f, noteDuration);
        _characterSliders[2].Init(5f, noteDuration);
        _characterSliders[1].Init(5f, noteDuration);
    }

    public bool IsInHitZone(float t)
    {
        return t < _hitZoneStart && t > _hitZoneEnd;
    }

    public CharacterSlider GetSliderByIndex(SliderIndex index) => _characterSliders[(int)index];
}
