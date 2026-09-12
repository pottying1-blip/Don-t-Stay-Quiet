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
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            Vector2 target = isClosed ? endPos.position : startPos.position;
            moveCoroutine = StartCoroutine(MoveDoor(target, doorMovingTime));
            isClosed = !isClosed;
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
