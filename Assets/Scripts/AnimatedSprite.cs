using UnityEngine.UI;
using UnityEngine;
public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Image _uiImage; // для анимации UI
    private int _frame;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float _framerate = 1f / 12f;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _uiImage = GetComponent<Image>();
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
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = sprites[_frame];
            }
            else if (_uiImage != null)
            {
                _uiImage.sprite = sprites[_frame];
            }
        }

    }
}
