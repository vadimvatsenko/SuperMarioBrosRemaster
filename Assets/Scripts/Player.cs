using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ScriptPlayerRender smallRenderer;
    [SerializeField] ScriptPlayerRender bigRenderer;
    private DeathAnimation _deathAnimation;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => _deathAnimation.enabled;

    private void Awake()
    {
        _deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit()
    {


        if (big)
        {
            Shrink();
        }
        if (small)
        {
            Death();
            Debug.Log("Enemy Hit");
        }

    }

    private void Shrink()
    {
        //
    }

    private void Death()
    {


        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        _deathAnimation.enabled = true;



        //GameManager.Instance.ResetLevel(3f); // с задержкой в 3сек
    }
}
