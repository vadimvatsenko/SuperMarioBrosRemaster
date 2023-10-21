using UnityEngine;

public class MarioController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private Animator animator;
    public float speedX = 1f; // скорость по оси X
    private float horizintal = 0f;
    private bool isFacingRight = true;

    //
    private bool isGround = false;
    private bool isJump = false;

    private bool isAcceleration = false;
    //

    const float speedMultiplier = 240f;

    void Awake()
    {

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizintal = Input.GetAxis("Horizontal");// возвращает от -1 до 1
        animator.SetFloat("HorizontalMove", Mathf.Abs(horizintal)); // означает, что в ту переменную, которую мы создали в юнити мы передаем значение horizintal(Mathf.Abs преобразовывает отрицательное число и положительное в положительное)
        //
        if (!isGround) // условие анимации прыжка, если обьект не на земле, присвой значение в "Jumping", true и проиграй анимацию
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
        //
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
        }
        //
        if (Input.GetKey(KeyCode.X) && isGround)
        {
            //isAcceleration = true;
            animator.SetBool("Running", true);
        }
        else
        {
            //isAcceleration = false;
            animator.SetBool("Running", false);
        }
        //

        if (horizintal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizintal < 0f && isFacingRight)
        {
            Flip();
        }

    }

    void FixedUpdate()
    {

        if (isJump)
        {
            rb.AddForce(new Vector2(0f, 500f));
            isGround = false;
            isJump = false;
        }
        if (isAcceleration)
        {
            rb.velocity = new Vector2(horizintal * (speedX * 4) * speedMultiplier * Time.fixedDeltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizintal * speedX * speedMultiplier * Time.fixedDeltaTime, rb.velocity.y);
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

}
