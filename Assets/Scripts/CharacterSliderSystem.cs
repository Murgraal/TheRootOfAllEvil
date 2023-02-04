using UnityEngine;

public class CharacterSliderSystem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _hitZoneTopRenderer;

    [SerializeField]
    private SpriteRenderer _hitZoneBottomRenderer;

    [Range(0, 1)]
    public float HitZoneStart;
    [Range(0,1)]
    public float HitZoneEnd;
    [Range(0.1f, 10f)]
    public float NoteDuration;

    [SerializeField]
    private float _sliderLength;


    [SerializeField]
    private CharacterSlider[] _characterSliders;

    public void Init()
    {
        _characterSliders[0].Init(_sliderLength, NoteDuration);
        _characterSliders[2].Init(_sliderLength, NoteDuration);
        _characterSliders[1].Init(_sliderLength, NoteDuration);

        _hitZoneTopRenderer.transform.position = _characterSliders[1].GetPos(HitZoneStart);
        _hitZoneBottomRenderer.transform.position = _characterSliders[1].GetPos(HitZoneEnd);

    }

    public bool IsInHitZone(float t)
    {
        return t > HitZoneStart && t < HitZoneEnd;
    }

    public CharacterSlider GetSliderByIndex(SliderIndex index) => _characterSliders[(int)index];
}
