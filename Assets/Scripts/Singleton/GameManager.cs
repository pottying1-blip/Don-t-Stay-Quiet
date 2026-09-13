using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float globalAlertLevel;

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
        
    }
}
