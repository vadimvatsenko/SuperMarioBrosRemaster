using UnityEngine;
public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D _rigitbody;
    public Vector2 direction = Vector2.left;
    private Vector2 _velocity;
    public float speed = 1f;
    private void Awake()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        _rigitbody.WakeUp();
    }
    private void OnDisable()
    {
        _rigitbody.velocity = Vector2.zero;
        _rigitbody.Sleep();
    }

    private void FixedUpdate()
    {
        _velocity.x = direction.x * speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        _rigitbody.MovePosition(_rigitbody.position + _velocity * Time.fixedDeltaTime);

        if (_rigitbody.Raycast(direction))
        {
            direction = -direction;
        }

        if (_rigitbody.Raycast(Vector2.down))
        {
            _velocity.y = Mathf.Max(_velocity.y, 0f);
        }
    }
}
