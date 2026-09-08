using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public Animator _brokenBottles;
    public AudioSource brokenSource;
    public AudioClip brokenSound;
    public bool isMakeNoise = false;
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
        isMakeNoise = true;
        if (isMakeNoise)
        {
            brokenSource.PlayOneShot(brokenSound);
            StartCoroutine(WaitNoNoise());
        }
    }

    IEnumerator WaitNoNoise()
    {
        yield return new WaitForSecondsRealtime(.5f);
        isMakeNoise = false;
    }


}
