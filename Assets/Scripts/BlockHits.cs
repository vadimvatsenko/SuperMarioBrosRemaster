using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHits : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] Sprite emptyBlock;
    [SerializeField] private int maxHits = -1;

    private Animator _animator;
    private bool _animating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        maxHits--;
        if (maxHits == 0)
        {
            _animator.enabled = false;
            spriteRenderer.sprite = emptyBlock;
        }
        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity); // используется для создания новых экземпляров объектов, Quaternion.identity означает, что новый объект будет создан с той же ориентацией, что и его префаб.
        }
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        _animating = true;
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        _animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t); // (Lerp) между начальной (from) и конечной (to) позициями в соответствии с нормализованным временем t. Это плавное изменение позиции объекта.
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = to;

    }
}
