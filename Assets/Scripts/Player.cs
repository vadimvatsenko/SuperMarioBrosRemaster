using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ScriptPlayerRender smallRenderer;
    [SerializeField] ScriptPlayerRender bigRenderer;
    private ScriptPlayerRender _activeRenderer;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource growSound;
    [SerializeField] AudioSource getLife;
    private DeathAnimation _deathAnimation;
    private CapsuleCollider2D _capsuleCollider;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => _deathAnimation.enabled;
    public bool starpower { get; private set; }

    private void Awake()
    {
        _deathAnimation = GetComponent<DeathAnimation>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _activeRenderer = smallRenderer;
    }

    public void Hit()
    {

        if (!starpower && !dead)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }


    private void Death()
    {

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        _deathAnimation.enabled = true;
        deathSound.Play();

        GameManager.Instance.ResetLevel(3f); // с задержкой в 3сек
    }

    public void Grow()
    {
        growSound.Play();
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        _activeRenderer = bigRenderer;

        _capsuleCollider.size = new Vector2(1f, 2f);
        _capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    private void Shrink()
    {
        growSound.Play();
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        _activeRenderer = smallRenderer;

        _capsuleCollider.size = new Vector2(1f, 1f);
        _capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !bigRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        _activeRenderer.enabled = true;
    }

    public void Starpower(float duration)
    {
        StartCoroutine(StarPowerAnimation(duration));

    }

    private IEnumerator StarPowerAnimation(float duration)
    {
        starpower = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                _activeRenderer._spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }

        _activeRenderer._spriteRenderer.color = Color.white;
        starpower = false;
    }
}
