using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioSource jumpSound;
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity; // переменная которая содержить x y кординаты нашего марио
    private float inputAxis; // переменная в которую будет записыватся значение 01 -1 до 1, в какая кнопка будет нажата(вперед или назад)

    public float moveSpeed = 8f; // скорость 8 юнитов
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f; // эти переменные нам позволят сделать прижек в зависимости от силы нажатия на кнопку прыжка
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2f);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Math.Abs(velocity.x) > 0.25f || Math.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // берем компонент по соседству
        camera = Camera.main; // получили доступ к главной камере
    }
    void Update()
    {
        HorizontalMovement(); // будет вызыватся каждый фрейм
        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovment();
        }
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        //Vector2 position = rigitbody.position
        //Здесь создается новая переменная position типа Vector2, которая предположительно используется для хранения текущей позиции объекта. 
        // rigidbody - это ссылка на компонент Rigidbody2D, который представляет физические свойства и тело объекта.

        //position += velocity * Time.fixedDeltaTime В этой строке кода происходит обновление позиции position. 
        //velocity предполагается как вектор скорости объекта. 
        //Умножение на Time.fixedDeltaTime гарантирует, что изменение позиции происходит в зависимости от фиксированного времени между фиксированными кадрами(FixedUpdate).
        // Это важно для обеспечения независимости от скорости обновления физики и остается стабильным на разных системах.

        // rigitbody.MovePosition(position) Здесь используется метод MovePosition компонента Rigidbody2D. 
        // Этот метод перемещает физическое тело объекта в указанную позицию position. 
        // Использование этого метода обеспечивает более стабильное и корректное обновление позиции объекта с учетом физики, 
        // так как он обрабатывает столкновения и другие аспекты физического моделирования.
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero); // получаем левую границу камеры
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal"); // записыватся значение 01 -1 до 1, в какая кнопка будет нажата(вперед или назад)

        //Mathf.MoveTowards(current, target, maxDelta) 
        // Mathf. Он используется для плавного изменения значения current к значению target
        // current - это текущее значение горизонтальной компоненты скорости (velocity.x).
        // target - это желаемое значение, к которому мы стремимся. 
        // В данном контексте, это inputAxis * moveSpeed, где inputAxis - это входной управляющий сигнал (например, нажатие клавиши), 
        //а moveSpeed - максимальная скорость, которую объект должен достичь.
        //maxDelta - это максимальное изменение, которое может произойти на этом шаге времени. 
        //В данном случае, moveSpeed * Time.deltaTime ограничивает, на сколько можно изменить current в текущем кадре, учитывая текущее время (Time.deltaTime).

        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x)) // передаем в наш внешний код, проверяем столкновение слева или справа, если правда, то скорость по x будет 0
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;

        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovment()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump"); // если высота игрока меньше нуля или не нажата кнопка прыжка, то значит мы падаем
        float multiplier = falling ? 2f : 1f; // мы падаем? если да то скорость падения будет 2, в противном случае 1ца
        velocity.y += gravity * multiplier * Time.deltaTime; // положение по y - гравитация * 2 или 1 * время
        //velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }



    private void OnCollisionEnter2D(Collision2D collision) // тут будет решатся проблема столновения головы с другими обьектами
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) // тут проверяем столкновение с головой врага
        {
            if (transform.DotTest(collision.transform, Vector2.down)) // передает во внешнюю функцию
            {
                velocity.y = jumpForce / 2f;// немного подпрыгивает на враге
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUP")) // если слой столкновения не является PowerUP, то выполни код
        {
            if (transform.DotTest(collision.transform, Vector2.up)) // если мы столкнулись головой с другим обьектом
            {
                velocity.y = 0f;
            }
        }
    }
}
