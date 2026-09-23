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
        
    }

    public Vector2 ReturnAlertButtonPos()
    {
        return this.transform.position;
    }
}
