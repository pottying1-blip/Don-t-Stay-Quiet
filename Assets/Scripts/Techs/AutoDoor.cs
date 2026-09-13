using System.Collections;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform autoDoor;
    public float doorMovingTime = 3.5f;
    public bool isClosed = true;
    private Coroutine moveCoroutine;
    private float detectRadius = 2f;
    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoMove();
    }

    void AutoMove()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        bool shouldBeOpen = false;
        foreach (Collider2D people in hits)
        {
            if (people.TryGetComponent<PlayerController>(out var player) && player.isPossessed 
            || people.CompareTag("Scientist")|| people.CompareTag("Soldier"))
            {
                shouldBeOpen = true;
                break;
            }
        }

        if (shouldBeOpen != isOpen) 
        {
            isOpen = shouldBeOpen;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);

            Vector2 target = isOpen ? endPos.position : startPos.position;
            moveCoroutine = StartCoroutine(MoveDoor(target, doorMovingTime));
        }
    
    }

    IEnumerator MoveDoor(Vector2 target, float duration)
    {
        float elapsedTime = 0f;
        Vector2 origin = autoDoor.position;
        while (elapsedTime < doorMovingTime)
        {
            elapsedTime+=Time.deltaTime;
            autoDoor.position = Vector2.Lerp(origin, target, elapsedTime/doorMovingTime);
            yield return null;
        }
        autoDoor.position = target;
    }
}
