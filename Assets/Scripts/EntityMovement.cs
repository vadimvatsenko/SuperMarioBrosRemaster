using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left; // так как враги начинают свое движение в лево
    [SerializeField] private float gravity = -9.81f;
    private Vector2 _velocity;
    private Rigidbody2D _rigitbody;

    private void Awake()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible() // функция в юнити, которая определяет видимость обьекта 
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
        _rigitbody.velocity = Vector2.zero; // остановка врага на позицию 0
        _rigitbody.Sleep();
    }

    private void FixedUpdate()
    {
        _velocity.x = direction.x * speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        _rigitbody.MovePosition(_rigitbody.position + _velocity * Time.fixedDeltaTime);

        if (_rigitbody.Raycast(direction))
        { // передаем во внешний класс, который обсчитывает столкновение с обьектами
            direction = -direction; // меняем направление
        }

        if (_rigitbody.Raycast(Vector2.down)) // если столкновение с землёй правда, то скорость движения по y - 0ль
        {
            _velocity.y = Mathf.Max(_velocity.y, 0f);
        }
    }
}
