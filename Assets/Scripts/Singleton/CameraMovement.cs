using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    public float shakeIntensity = 0.5f;
    private Vector3 newPos;
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
            StartCoroutine(ScreenShake());
        }
    }
    
    IEnumerator ScreenShake()
    {
        Vector2 randomShake = Random.insideUnitCircle * shakeIntensity;
        transform.position = newPos + (Vector3)randomShake;
        yield return new WaitForSeconds(0.25f);
        transform.position = newPos;
        playerController.isTakeDamage =  false;
    }
}
