using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public Animator _brokenBottles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _brokenBottles.SetBool("isBroken", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Broken()
    {
        _brokenBottles.SetBool("isBroken", true);
    }
}
