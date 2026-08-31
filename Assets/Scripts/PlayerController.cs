using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb2d;
    public float moveSpeed = 15f;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    public float invDuration = 2f;
    public bool isInvisible = false;
    private float coyoteTime = 0.5f;
    private float coyoteTimeCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (rb2d.linearVelocity != Vector2.zero) {coyoteTimeCounter = 0;}
        else {coyoteTimeCounter+=Time.deltaTime;}

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontalInput, verticalInput).normalized;    

        TurnInvisible();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Movement();
    }

    void Movement()
    {

        if (moveInput != Vector2.zero)
        {
            rb2d.linearVelocity = moveInput*moveSpeed;
        }
        else {rb2d.linearVelocity = Vector2.zero;}
        
    }

    void TurnInvisible()
    {
        if (coyoteTimeCounter > coyoteTime && !isInvisible)
        {
            isInvisible = true;
            StartCoroutine(SlowlyFadeInvisible(0.0075f, invDuration = 0.65f));
        }

        if (rb2d.linearVelocity != Vector2.zero && isInvisible)
        {
            isInvisible = false;
            StartCoroutine(SlowlyReturn(1f, invDuration = 0.35f));
        }
    }

    IEnumerator SlowlyFadeInvisible(float targetAlpha, float invDuration)
    {
        if (isInvisible)
        {
            
            Color c = spriteRenderer.color;
            float startDuration = 0;
            float startAlpha = c.a;
            //float startAlpha = 0.01f;
            //float endAlpha = 1f;
            while (startDuration < invDuration)
            {
                startDuration +=Time.deltaTime;
                c.a = Mathf.Lerp(startAlpha, targetAlpha, startDuration/invDuration);
                spriteRenderer.color = c;
                yield return null;
            }

            if (c.a == 1f)
            {
                c.a = 0f;
            }
        }
    }

    IEnumerator SlowlyReturn(float targetAlpha, float invDuration)
    {
        if (!isInvisible)
        {
            Color c = spriteRenderer.color;
            float startDuration = 0;
            float startAlpha = c.a;
            //float startAlpha = 0.01f;
            //float endAlpha = 1f;
            while (startDuration < invDuration)
            {
                startDuration +=Time.deltaTime;
                c.a = Mathf.Lerp(startAlpha, targetAlpha, startDuration/invDuration);
                spriteRenderer.color = c;
                yield return null;
            }

            if (c.a == 0f)
            {
                c.a = 1f;
            }
        }
    }

}
