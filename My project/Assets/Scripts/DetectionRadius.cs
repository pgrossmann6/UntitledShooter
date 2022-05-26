using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
    public string otherTag;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == otherTag)
        {
            NavMeshAI navMeshAI = GetComponentInParent<NavMeshAI>();
            if  (navMeshAI.target == null)
            {
                GetComponentInParent<NavMeshAI>().target = other.transform;
            }
            else if (Vector3.Distance(navMeshAI.target.position, transform.position) > Vector3.Distance(other.transform.position, transform.position))
            {
                GetComponentInParent<NavMeshAI>().target = other.transform;
            }
        }
    }
}
