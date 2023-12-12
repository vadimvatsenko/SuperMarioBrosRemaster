using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] private Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower)
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // это внешний класс, который проверяет, столкновение марио с головой гумбы
            {
                Flatten();
            }
            else
            {
                Debug.Log("Гумба ударил Марио");
                player.Hit();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Flatten() // метод сплющивания Гумбы
    {
        GetComponent<Collider2D>().enabled = false; // отключаем коллайдер
        GetComponent<EntityMovement>().enabled = false; // отключаем движения
        GetComponent<AnimatedSprite>().enabled = false; // отключаем анимацию спрайтов
        GetComponent<SpriteRenderer>().sprite = flatSprite; // получаем сплющеный Гумба
        Destroy(gameObject, 0.5f); // уничтожь гумбу через пол секунды
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBacameInvisible()
    {

    }
}
