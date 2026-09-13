using System.Numerics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject consumeGuidance;
    public Transform playerPos;
    private Camera mainCamera;
    private UnityEngine.Vector2 offset = new UnityEngine.Vector2(0f, -130f);
    public GameObject dialogue;
    void Awake()
    {
        mainCamera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        UnityEngine.Vector3 truePlayerPos = mainCamera.WorldToScreenPoint(playerPos.position);
        consumeGuidance.transform.position = truePlayerPos + (UnityEngine.Vector3)offset;
    }
}
