using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float globalAlertLevel;
    public float globalAlertThreshold = 5f;
    public bool WarningStart = false;
    public List<Transform> allAlertButtons;
    public Transform currentAlert;
    public bool hasAlert = false;
    public AudioSource gameAudioSource;
    public AudioClip warningSound;
    public bool hasPlayAlert = false;
    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (globalAlertLevel >= globalAlertThreshold)
        {
            WarningStart = true;
        }

        if (hasAlert && !hasPlayAlert)
        {
            gameAudioSource.PlayOneShot(warningSound);
            hasPlayAlert = true;
        }

        /*if (WarningStart)
        {
            Debug.Log("CANH BAO, CO VAT THE");
            WarningStart = false;
            globalAlertLevel = 0;
        }*/
    }
}
