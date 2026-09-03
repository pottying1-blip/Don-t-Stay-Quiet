using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb2d;
    public float moveSpeed = 7f;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    public float invDuration = 2f;
    public bool isInvisible = false;
    private float coyoteTime = 0.5f;
    private float coyoteTimeCounter;
    public bool isCrouch = false;
    public float crouchSpeed = 2f;
    private Vector2 mousePos;
    private Vector2 worldMousePos;
    private Vector2 playerPosition;
    [SerializeField]private LayerMask interactableLayer;
    private InteractableObject interactableObject;
    private float throwAngle;
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
        if (rb2d.linearVelocity.magnitude > crouchSpeed) {coyoteTimeCounter = 0;}
        else {coyoteTimeCounter+=Time.deltaTime;}

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontalInput, verticalInput).normalized; 
        
        mousePos = Input.mousePosition;
        worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 mouseAndPlayerDistance = worldMousePos - playerPosition;
        throwAngle = Mathf.Atan2(mouseAndPlayerDistance.y, mouseAndPlayerDistance.x)*Mathf.Rad2Deg;
        playerPosition = transform.position;

        CrouchMovement();
        TurnInvisible();
        CheckInteraction();
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
            if (!isCrouch)
            rb2d.linearVelocity = moveInput*moveSpeed;

            else rb2d.linearVelocity = moveInput*crouchSpeed;
        }
        else {rb2d.linearVelocity = Vector2.zero;}
    }

    void CheckInteraction()
    {
        float pickUpDistance = 2f;
        Collider2D interactObj = Physics2D.OverlapPoint(worldMousePos, interactableLayer);

        if (interactableObject != null)
        {
            interactableObject.FollowPlayer(playerPosition, Quaternion.Euler(0f, 0f, throwAngle));
            if (Input.GetKeyDown(KeyCode.Mouse0)&& interactableObject.isHeld)
            {
                interactableObject.Throw();
                interactableObject = null;
            }
            return;
        }

        if (interactObj != null)
        {
            float trueDistance = Vector2.Distance(playerPosition, interactObj.transform.position);
            if (trueDistance < pickUpDistance && Input.GetKeyDown(KeyCode.Mouse0))
            {
                interactableObject = interactObj.GetComponent<InteractableObject>();
                interactableObject.Pickup();
            }
        }
    }

    void CrouchMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCrouch)
        {
            isCrouch = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && isCrouch)
        {
            isCrouch = false;
        }
    }


    void TurnInvisible()
    {
        if (coyoteTimeCounter > coyoteTime && !isInvisible)
        {
            isInvisible = true;
            StartCoroutine(SlowlyFadeInvisible(0.02f, invDuration = 0.65f));
        }

        if (rb2d.linearVelocity.magnitude > crouchSpeed && isInvisible)
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
