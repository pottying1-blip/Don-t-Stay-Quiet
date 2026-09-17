using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float globalAlertLevel;
    public float globalAlertThreshold = 5f;
    public bool WarningStart = false;
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

        if (WarningStart)
        {
            Debug.Log("CANH BAO, CO VAT THE");
            WarningStart = false;
            globalAlertLevel = 0;
        }
    }
}
