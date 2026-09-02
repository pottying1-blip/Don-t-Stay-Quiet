using System.Numerics;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable 
{
    private SpriteRenderer spriteRenderer;

    public bool isGrabable;
    private Collider2D objCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    public void Interaction()
    {

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
