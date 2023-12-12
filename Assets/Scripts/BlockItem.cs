using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        // получаем свойства компонентов
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D phisicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // так как нам нужно отключить свойсва компонентов, потому что они будут спрятаны за другим компонентом и будут бится коллайдерами друг друга
        rigidbody.isKinematic = true;
        phisicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f); // для приостановки выполнения кода на определенное количество секунд в корутинах

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null; // приостановить выполнение на один кадр и продолжить на следующем
        }

        transform.localPosition = endPosition;
        rigidbody.isKinematic = false;
        phisicsCollider.enabled = true;
        triggerCollider.enabled = true;

    }
}
