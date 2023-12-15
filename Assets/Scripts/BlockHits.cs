using System.Collections;
using UnityEngine;

public class BlockHits : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _brickBroke;
    [SerializeField] GameObject item;
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
        BoxCollider2D _boxCollider2d = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // доступ к SpriteRenderer, если он находится на элементе
        spriteRenderer.enabled = true;
        maxHits--;
        if (maxHits == 0)
        {
            if (_animator != null)
            {
                _animator.enabled = false;
            }

            spriteRenderer.sprite = emptyBlock;
        }
        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        if (destroyBlock != null && player.big)
        {
            //_brickBroke.Play();
            spriteRenderer.enabled = false;
            //_boxCollider2d.enabled = false;
            Debug.Log("Big Mario Hit Block");
            Instantiate(destroyBlock, transform.position, Quaternion.identity);
            Destroy(gameObject);
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

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = to;

    }
}
