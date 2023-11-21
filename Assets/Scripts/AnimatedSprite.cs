using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float _framerate = 1f / 6f; // будет переключатся между шестью кадрами в секунду

    private SpriteRenderer _spriteRenderer;
    private int _frame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), _framerate, _framerate);
        //InvokeRepeating: 
        //Это метод в Unity, который запускает указанный метод через определенные интервалы времени.

        //nameof(Animate): 
        //nameof - это оператор C#, который возвращает строку с именем переменной, типа или члена. 
        //В данном случае, nameof(Animate) возвращает строку с именем метода Animate. 
        //Animate предполагается, что это метод, который вы хотите вызвать повторяющимся образом.

        //_framerate: 
        //Это переменная или константа, предположительно, представляющая интервал времени в секундах между вызовами метода Animate.

        //_framerate: 
        //Это также интервал времени, но указанный второй раз, который представляет период повторения вызова метода. 
        //Таким образом, _framerate указывается дважды, первый раз указывает начальную задержку перед первым вызовом, 
        //а второй раз указывает период повторения.
    }

    private void OnDisable()
    {
        CancelInvoke();
        // CancelInvoke - это метод в Unity, который используется для отмены всех повторяющихся вызовов, 
        // созданных с помощью методов InvokeRepeating или Invoke. 
        // Этот метод принадлежит классу MonoBehaviour, 
        // и его цель - прекратить вызовы определенных методов в определенном объекте.
    }

    private void Animate()
    {
        _frame++;

        if (_frame >= sprites.Length)
        {
            _frame = 0;
        }
        if (_frame >= 0 && _frame < sprites.Length)
            _spriteRenderer.sprite = sprites[_frame];
    }

}
