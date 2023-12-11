using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate()); // StartCoroutine используется для запуска выполнения корутин в Unity. 
                                   //Она принимает в качестве аргумента метод, который возвращает IEnumerator 
                                   //(тип, который возвращает метод, используемый для создания корутины). 
                                   //В вашем случае, метод Animate() возвращает IEnumerator, 
                                   //что делает его пригодным для использования с StartCoroutine
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10; // при анимации смерти героя нам нужно установить приоритет героя в максимально возможный на сцене, чтобы он был поверх всего

        if (deathSprite != null)
        {
            spriteRenderer.sprite = deathSprite;
        }

    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }
        // почему именнно такой код? потому что, этот код универсален для марио и врагов, а скрипты PlayerMovement и EntityMovement есть только у Марио, потому они будут отключатся у него


        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    //Этот код представляет собой корутину в языке программирования C#, который, вероятно, 
    //используется в Unity для анимации прыжка объекта вверх и его последующего падения под воздействием гравитации. 
    //Давайте рассмотрим, как работает каждая часть кода:

    //IEnumerator Animate(): Это метод, который возвращает IEnumerator.
    //В Unity корутины используются для выполнения задач по времени, таких как анимация.

    //float elapsed = 0f;: Переменная elapsed используется для отслеживания прошедшего времени с момента начала анимации.
    //float duration = 3f;: Переменная duration определяет общую продолжительность анимации в секундах(в данном случае, 3 секунды).
    //float jumpVelocity = 10f;: jumpVelocity представляет собой начальную скорость прыжка вверх.
    //float gravity = -36f;: gravity определяет ускорение свободного падения из-за гравитации.

    //Vector3 velocity = Vector3.up * jumpVelocity;: velocity - это вектор скорости, который начинается с направленной вверх скорости, заданной jumpVelocity.

    //while (elapsed<duration) { ... }: Это цикл while, который будет выполняться, пока не пройдет общее время анимации (elapsed меньше duration).

    //transform.position += velocity * Time.deltaTime;: Обновляет позицию объекта, добавляя к текущей позиции произведение вектора скорости (velocity) на текущий интервал времени (Time.deltaTime).
    //velocity.y += gravity * Time.deltaTime;: Обновляет компоненту y вектора скорости, добавляя гравитационное ускорение.

    //elapsed += Time.deltaTime;: Увеличивает прошедшее время.

    //yield return null;: Приостанавливает выполнение корутины на один кадр, передавая управление следующему кадру.
    //Когда время elapsed становится больше или равно duration, цикл завершается, и анимация завершается.
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
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
