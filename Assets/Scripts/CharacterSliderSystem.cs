using UnityEngine;

public class CharacterSliderSystem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _hitZoneTopRenderer;

    [SerializeField]
    private SpriteRenderer _hitZoneBottomRenderer;

    [Range(0, 1)]
    [SerializeField]
    private float _hitZoneStart;

    [Range(0,1)]
    [SerializeField]
    private float _hitZoneEnd;

    [SerializeField]
    private float _sliderLength;

    [SerializeField]
    private float _noteDuration;

    [SerializeField]
    private CharacterSlider[] _characterSliders;

    public void Init()
    {
        _characterSliders[0].Init(_sliderLength, _noteDuration);
        _characterSliders[2].Init(_sliderLength, _noteDuration);
        _characterSliders[1].Init(_sliderLength, _noteDuration);

        _hitZoneTopRenderer.transform.position = _characterSliders[1].GetPos(_hitZoneStart);
        _hitZoneBottomRenderer.transform.position = _characterSliders[1].GetPos(_hitZoneEnd);

    }

    public bool IsInHitZone(float t)
    {
        return t < _hitZoneStart && t > _hitZoneEnd;
    }

    public CharacterSlider GetSliderByIndex(SliderIndex index) => _characterSliders[(int)index];
}
