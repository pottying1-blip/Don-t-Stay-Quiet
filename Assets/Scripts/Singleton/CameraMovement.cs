using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public float shakeIntensity = 0.1f;
    private Vector3 newPos;
    [SerializeField]private float frequency = 25f;
    [SerializeField]private float maxOffset = 1f;
    [SerializeField]private float recoverSpeed = 1.5f;
    [SerializeField]private float traumaExponent = 1.75f;
    float trauma = 0f;
    float seed;

    void Awake()
    {
        seed = UnityEngine.Random.value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        newPos = player.position;
        newPos.z = transform.position.z;
        transform.position = newPos;

        if (playerController.isTakeDamage)
        {
            trauma = Mathf.Clamp01(trauma + 0.4f); //chặn trên
            playerController.isTakeDamage = false;
        }

        float shake = Mathf.Pow(trauma, traumaExponent);
        float offsetX = maxOffset * (Mathf.PerlinNoise(seed, Time.time * frequency)*2 - 1);
        float offsetY = maxOffset * (Mathf.PerlinNoise(seed + 1f, Time.time * frequency)*2 - 1);

        transform.position = newPos + new Vector3(offsetX, offsetY, 0)* shake;
        trauma = Mathf.Max(0f, trauma - recoverSpeed * Time.deltaTime); //chặn dưới
    }

}
