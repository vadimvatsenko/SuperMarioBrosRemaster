using UnityEngine;

public class ScriptPlayerRender : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer { get; private set; }
    private PlayerMovement _playerMovement;

    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _jump;
    [SerializeField] private Sprite _slide;
    [SerializeField] private Sprite _InFlag;
    [SerializeField] private AnimatedSprite _run;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
        _run.enabled = false;
    }

    private void LateUpdate()
    {
        _run.enabled = _playerMovement.running;
        if (_playerMovement.jumping)
        {
            _spriteRenderer.sprite = _jump;
        }
        else if (_playerMovement.sliding)
        {
            _spriteRenderer.sprite = _slide;
        }
        else if (!_playerMovement.running)
        {
            _spriteRenderer.sprite = _idle;
        }

    }

    public void SetInFlagSprite()
    {
        _spriteRenderer.sprite = _InFlag;
    }
}
