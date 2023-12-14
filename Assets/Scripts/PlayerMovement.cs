using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private Collider2D _collider;
    private Vector2 _velocity;
    [SerializeField] AudioSource jumpSound;

    private float inputAxis;
    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2f);

    public bool jumping { get; private set; }
    public bool grounded { get; private set; }
    public bool running => Math.Abs(_velocity.x) > 0.25f || Math.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && _velocity.x < 0f) || (inputAxis < 0f && _velocity.x > 0f);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
        _velocity = Vector2.zero;
        jumping = false;
    }
    private void OnDisable()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _velocity = Vector2.zero;
        jumping = false;

    }

    private void Update()
    {
        HorizontalMovement();
        grounded = _rigidbody.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovment();
        }
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;
        position += _velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = _camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        _rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        _velocity.x = Mathf.MoveTowards(_velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (_rigidbody.Raycast(Vector2.right * _velocity.x))
        {
            _velocity.x = 0f;
        }

        if (_velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;

        }
        else if (_velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovment()
    {
        _velocity.y = Mathf.Max(_velocity.y, 0f);
        jumping = _velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
            _velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = _velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        _velocity.y += gravity * multiplier * Time.deltaTime;
        //velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                _velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUP"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                _velocity.y = 0f;
            }
        }
    }
}
