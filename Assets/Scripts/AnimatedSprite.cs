using UnityEngine;
public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int _frame;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float _framerate = 1f / 12f;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), _framerate, _framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        _frame++;

        if (_frame >= sprites.Length)
        {
            _frame = 0;
        }
        if (_frame >= 0 && _frame < sprites.Length)
            _spriteRenderer.sprite = sprites[_frame];
    }
}
