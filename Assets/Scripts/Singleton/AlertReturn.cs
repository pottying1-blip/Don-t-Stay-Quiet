using System.Collections;
using UnityEngine;

public class AlertReturn : MonoBehaviour
{
    public bool isResolved = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isResolved)
        {
            StartCoroutine(ReturnState());
        }
    }

    IEnumerator ReturnState()
    {
        yield return new WaitForSecondsRealtime(6f);
        isResolved = false;
    }
}
