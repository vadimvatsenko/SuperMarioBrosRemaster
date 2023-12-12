using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    [SerializeField] private Sprite shellSprite;
    [SerializeField] private float shellSpeed = 12f;
    private bool _shelled;
    private bool _shellMoving;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.starpower)
            {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down)) // это внешний класс, который проверяет, столкновение марио с головой гумбы
            {
                EnterShell();
            }
            else
            {
                player.Hit();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_shelled && other.CompareTag("Player")) // если купа в панцире и столкнулся с Игроком
        {
            if (!_shellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>(); // в противном случае Игрок покучит урон
                if (player.starpower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }

            }
        }
        else if (!_shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void EnterShell()
    { // метод который превращает Купа в панцирь

        _shelled = true;
        GetComponent<EntityMovement>().enabled = false; // отключаем движения
        GetComponent<AnimatedSprite>().enabled = false; // отключаем анимацию спрайтов
        GetComponent<SpriteRenderer>().sprite = shellSprite; // получаем спрайт панциря


    }

    private void PushShell(Vector2 direction)
    {
        _shellMoving = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Shell"); // когда купа превращается в панцир, мы пеняем слой на Shell 
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBacameInvisible()
    {
        if (_shellMoving)
        {
            Destroy(gameObject);
        }
    }


}
