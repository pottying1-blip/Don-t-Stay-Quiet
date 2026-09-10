using System;
using System.Collections;
using System.Linq;
using Unity.Jobs;
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
    [SerializeField]private LayerMask npcLayer;
    [SerializeField]private GameObject consumeGuidanceCanvas;
    public bool hasShownConsumeGuidance = false;
    private InteractableObject interactableObject;
    private float throwAngle;
    public HumanStateManager humanStateManager;
    private float pierceDistance = 3f;
    private float timerQPress = 1.25f;
    private float holdTime = 0f;
    public AudioSource playerSoundSource;
    public AudioClip eatingSound;
    public AudioClip possessSound;
    private Sprite targetSprite;
    private bool actionTrigerred = false;
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
        KillNPC();
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

    void KillNPC()
    {
        Collider2D[] livingThings = Physics2D.OverlapCircleAll(playerPosition, pierceDistance, npcLayer);

        if (livingThings.Length > 0)
        {
            HumanStateManager target = FindClosestTarget(livingThings);
            Collider2D targetCollider = target.GetComponent<Collider2D>();
            
            if (target != null)
            {
                float distance = Vector2.Distance(playerPosition, target.transform.position);
                HandleKillInput(target, distance, targetCollider);
                HandleConsumeGuidance(target, distance);
            }
        }
        HandleConsumeInput();
    }

    void HandleKillInput(HumanStateManager target, float distance, Collider2D targetCollider)
    {
        if (target.isMakingNoises && Input.GetKeyDown(KeyCode.Mouse0) && !target.isDead && distance < pierceDistance)
        {
            Vector2 humanPos = target.transform.position;
            transform.position = humanPos + new Vector2(1f, 0f);
            targetSprite = target.GetComponent<SpriteRenderer>().sprite;
            target.Die();
            targetCollider.isTrigger = true;
            humanStateManager = target; 
        }
    }

    void HandleConsumeGuidance(HumanStateManager target, float distance)
    {
        if (!consumeGuidanceCanvas.activeSelf && target.isDead && distance < pierceDistance)
        {
            consumeGuidanceCanvas.SetActive(true);
            hasShownConsumeGuidance = true;
        }

        if (hasShownConsumeGuidance && distance > pierceDistance)
        {
            consumeGuidanceCanvas.SetActive(false);
            hasShownConsumeGuidance = false;
        }
    }

    void HandleConsumeInput()
    {
        Vector2 growSize = new Vector2(0.2f, 0.2f);
        if (Input.GetKey(KeyCode.Q) && hasShownConsumeGuidance && humanStateManager != null)
        {
            holdTime+= Time.deltaTime;
            if (holdTime >= timerQPress && !actionTrigerred)
            {
                actionTrigerred = true;
                playerSoundSource.PlayOneShot(possessSound);
                transform.position = humanStateManager.transform.position;
                spriteRenderer.sprite = targetSprite;
                StartCoroutine(WaitForDelete());
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!actionTrigerred && hasShownConsumeGuidance && humanStateManager != null && holdTime < timerQPress)
            {
                playerSoundSource.PlayOneShot(eatingSound);
                transform.localScale = (Vector2)transform.localScale + growSize;
                transform.position = humanStateManager.transform.position;
                Destroy(humanStateManager.gameObject);
                consumeGuidanceCanvas.SetActive(false);
                humanStateManager = null; 
            }
        holdTime = 0f;
        actionTrigerred = false;
        }
        
    }

    IEnumerator WaitForDelete(){
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(humanStateManager.gameObject);
        consumeGuidanceCanvas.SetActive(false);
        humanStateManager = null; 
    }

    HumanStateManager FindClosestTarget(Collider2D[] candidates)
    {
        HumanStateManager closest = null;
        float closestDist = float.MaxValue;

        foreach (Collider2D thing in candidates)
        {
            if (thing.TryGetComponent<HumanStateManager>(out var hsm))
            {
                float d = Vector2.Distance(playerPosition, hsm.transform.position);
                if (d < closestDist)
                {
                    closestDist = d;
                    closest = hsm;
                }
            }
        }
        return closest;
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
