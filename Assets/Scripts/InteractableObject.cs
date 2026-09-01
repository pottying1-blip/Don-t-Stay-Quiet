using UnityEngine;

public class ItemGeneral : MonoBehaviour, IInteractable 
{
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Interaction()
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
