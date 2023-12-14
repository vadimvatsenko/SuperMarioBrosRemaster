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
            else if (collision.transform.DotTest(transform, Vector2.down))
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
        if (_shelled && other.CompareTag("Player"))
        {
            if (!_shellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();
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
    {
        _shelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        _shellMoving = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
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
