using System;
using System.Collections;
using System.Numerics;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable 
{
    private SpriteRenderer spriteRenderer;

    public bool isGrabable;
    private Collider2D objCollider;
    private Transform objPos;
    public bool isHeld;
    private Rigidbody2D rb2d;
    public float throwForce = 5f;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interaction(UnityEngine.Vector2 playerPos)
    {
        
    }

    public void Throw()
    {
        rb2d.AddRelativeForce(UnityEngine.Vector2.right * throwForce, ForceMode2D.Impulse);
    }

    public void FollowPlayer(UnityEngine.Vector2 playerPos, UnityEngine.Quaternion playerRotation)
    {
        if (isHeld)
        {
            transform.rotation = playerRotation;
            transform.position = playerPos + new UnityEngine.Vector2(0.5f, 0.5f);
        }
    }
    public void Pickup()
    {
        isHeld = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        BreakableObject breakableObject = GetComponent<BreakableObject>();
        if (breakableObject != null)
        {
            breakableObject.Broken();
            StartCoroutine(DestroyAfterHit());
        }
    }

    IEnumerator DestroyAfterHit()
    {
        rb2d.linearVelocity = UnityEngine.Vector2.zero;
        yield return new WaitForSecondsRealtime(3.5f);
        Destroy(gameObject);
    }

    void TurnOnOutline()
    {
        spriteRenderer.material.SetFloat("_OutlineEnable", 1f);
    }

    void TurnOffOutline()
    {
        spriteRenderer.material.SetFloat("_OutlineEnable", 0f);
    }

    void OnMouseEnter()
    {
        TurnOnOutline(); 
    }

    void OnMouseExit()
    {
        TurnOffOutline();
    }

    
}
