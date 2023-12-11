using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Vector2 direction = Vector2.left;
    [SerializeField] private float gravity = -9.81f;

    private Vector2 _velocity;
    private Rigidbody2D _rigitbody;

    private void Awake()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _velocity.x = direction.x * speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        _rigitbody.MovePosition(_rigitbody.position + _velocity * Time.fixedDeltaTime);
    }
}
