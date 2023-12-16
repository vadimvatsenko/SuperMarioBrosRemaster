using System.Collections;
using UnityEngine;

public class BlockHits : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _brickBroke;
    [SerializeField] GameObject firstItem;
    [SerializeField] GameObject secondItem;
    [SerializeField] GameObject destroyBlock;

    [SerializeField] Sprite emptyBlock;

    [SerializeField] private int maxHits = -1;
    private bool _animating;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (!_animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit(player);
            }
        }
    }

    private void Hit(Player player)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // доступ к SpriteRenderer, если он находится на элементе

        if (player.big && destroyBlock != null && !firstItem)
        {
            spriteRenderer.enabled = false;
            StartCoroutine(Animate(player));
            Instantiate(destroyBlock, transform.position, Quaternion.identity);
        }
        else
        {
            spriteRenderer.enabled = true;
            maxHits--;

            if (maxHits == 0)
            {
                spriteRenderer.sprite = emptyBlock;

                if (_animator != null)
                {
                    _animator.enabled = false;
                }

            }

            if (secondItem && player.big)
            {
                Instantiate(secondItem, transform.position, Quaternion.identity);
            }
            else if (!secondItem && firstItem && player.big || player.small)
            {
                Instantiate(firstItem, transform.position, Quaternion.identity); // экземпляр который создает блок из префаба
            }

            
            StartCoroutine(Animate(player));
        }

    }

    private IEnumerator Animate(Player player)
    {
        _animating = true;
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        _animating = false;
        if (player.big && destroyBlock != null && !firstItem)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = to;

    }
}
