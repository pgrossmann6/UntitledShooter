using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAI AI;
    void Start()
    {
        AI = GetComponent<NavMeshAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AI.target == null)
        {
            FindTarget();
        }
        else
        {
            Vector3 lookPosition = AI.target.position;
            lookPosition.y = transform.position.y;
            Vector3 lookDirection = (lookPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
        }
    }

    private void FindTarget()
    {

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, 20f);
        foreach (Collider collider in colliderArray)
        {
            //Debug.Log(collider.name);
            if (collider.tag =="Enemy")
            {
                if (AI.target == null)
                {
                    AI.target = collider.transform;

                }
                else if (Vector3.Distance(collider.transform.position, transform.position) < Vector3.Distance(AI.target.position, transform.position))
                {
                    AI.target = collider.transform;
                }
            }

        }

    }
}
