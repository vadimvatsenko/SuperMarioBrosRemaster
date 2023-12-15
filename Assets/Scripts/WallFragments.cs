using System.Collections;
using UnityEngine;

public class WallFragments : MonoBehaviour
{
    [SerializeField] private float deviationX;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = 10;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;
        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;



        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            velocity.x += deviationX;
            elapsed += Time.deltaTime;
            yield return null;


        }

        Destroy(gameObject); // удаляем объект после анимации
    }


}


