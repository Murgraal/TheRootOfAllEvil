using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField]
    private HitEffect _effectPrefab;

    [SerializeField]
    private Sprite[] _sprites;

    public void SpawnHitEffect(Vector3 pos)
    {
        var index = (int)Data.GamePlay.MultiplierLevel;
        SpawnEffect(pos, index);
    }
    public void SpawnMissEffect(Vector3 pos)
    {
        SpawnEffect(pos, _sprites.Length - 1);
    }

    public void SpawnEffect(Vector3 pos, int index)
    {
        var hitEffect = GameObject.Instantiate(_effectPrefab, pos, Quaternion.identity);
        hitEffect.SetSprite(_sprites[index]);
    }
}
