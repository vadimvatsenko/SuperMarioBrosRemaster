
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity; // переменная которая содержить x y кординаты нашего марио
    private float inputAxis; // переменная в которую будет записыватся значение 01 -1 до 1, в какая кнопка будет нажата(вперед или назад)

    public float moveSpeed = 8f; // скорость 8 юнитов

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // берем компонент по соседству
        camera = Camera.main; // получили доступ к главной камере
    }
    void Update()
    {
        HorizontalMovement(); // будет вызыватся каждый фрейм
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
}
