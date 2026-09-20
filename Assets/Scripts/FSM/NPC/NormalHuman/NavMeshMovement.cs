using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform targetPosTransform;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetPosTransform.transform.position;
    }
}
