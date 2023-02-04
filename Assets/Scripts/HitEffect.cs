using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("ScaleUpCourotine");
    }
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.ResetBounds();
    }

    public IEnumerator ScaleUpCourotine()
    {
        float elapsedTime = 0f;
        Color currentColor = _spriteRenderer.material.color;

        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            transform.position += (Vector3)Random.insideUnitCircle * Time.deltaTime;
            transform.localScale += Vector3.one * 1f * Time.deltaTime;
            currentColor.a = Mathf.Lerp(1, 0, elapsedTime / 2f);
            _spriteRenderer.material.color = currentColor;
            yield return null;
        }
        Destroy(gameObject);
    }

}