using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    [SerializeField] public Transform target;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) {return;}
        if (navMeshAgent == null) {return;}
        
        navMeshAgent.destination = target.position;
    }
}
