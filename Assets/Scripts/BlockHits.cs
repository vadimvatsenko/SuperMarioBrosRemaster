using System.Collections;
using UnityEngine;

public class BlockHits : MonoBehaviour
{
    [SerializeField] AudioSource _brickBroke;
    [SerializeField] GameObject firstItem;
    [SerializeField] GameObject secondItem;
    [SerializeField] GameObject destroyBlock;
    [SerializeField] Sprite emptyBlock;
    [SerializeField] private int maxHits = -1;
    private AnimatedSprite _animatedSprite;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animatedSprite = GetComponent<AnimatedSprite>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit(player);
            }
        }
    }

    private void Hit(Player player)
    {
        maxHits--;
        if (player.small)
        {
            if (maxHits < 0 && !firstItem && !secondItem)
            {
                StartCoroutine(Animate(player));
            }
            else if (maxHits > 0 && firstItem)
            {
                StartCoroutine(Animate(player));
                Instantiate(firstItem, transform.position, Quaternion.identity);
            }
            else if (maxHits == 0)
            {
                StartCoroutine(Animate(player));
                Instantiate(firstItem, transform.position, Quaternion.identity);
                if (_animatedSprite)
                {
                    _animatedSprite.enabled = false;
                }
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = emptyBlock;

            }
        }
        if (player.big || player.super)
        {
            Debug.Log(player.super);
            if (maxHits < 0 && !firstItem && !secondItem)
            {
                _brickBroke.Play();
                StartCoroutine(Animate(player));
                Instantiate(destroyBlock, transform.position, Quaternion.identity);
                _spriteRenderer.enabled = false;
            }
            else if (maxHits > 0 && firstItem)
            {
                StartCoroutine(Animate(player));
                Instantiate(firstItem, transform.position, Quaternion.identity);
            }
            else if (maxHits == 0)
            {
                StartCoroutine(Animate(player));
                if (firstItem && !secondItem)
                {
                    Instantiate(firstItem, transform.position, Quaternion.identity);
                }
                if (secondItem)
                {
                    Instantiate(secondItem, transform.position, Quaternion.identity);
                }

                if (_animatedSprite)
                {
                    _animatedSprite.enabled = false;
                }
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = emptyBlock;
            }
        }
    }

    private IEnumerator Animate(Player player)
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        if ((player.big || player.super) && maxHits < 0 && !firstItem && !secondItem)
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
