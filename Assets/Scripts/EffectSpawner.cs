using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField]
    private HitEffect _effectPrefab;

    [SerializeField]
    private Sprite[] _sprites;

    public void SpawnEffect(Vector3 pos)
    {
        var randomIndex = Random.Range(0, _sprites.Length);
        var hitEffect = GameObject.Instantiate(_effectPrefab, pos, Quaternion.identity);
        hitEffect.SetSprite(_sprites[randomIndex]);
    }
}
