using UnityEngine;

public class ScriptPlayerRender : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer; // переменная для рендера спрайтов
    private PlayerMovement _playerMovement; // переменная которая будет хранить в себе скрипт

    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _jump;
    [SerializeField] private Sprite _slide;
    [SerializeField] private AnimatedSprite _run;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponentInParent<PlayerMovement>(); // получить компонент в родителе
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
}
