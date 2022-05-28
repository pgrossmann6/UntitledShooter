using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    [SerializeField] public Transform target;
    public NavMeshAgent navMeshAgent;
    private Animator _anim;
    public string state;
    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        state = "Idle";

    }

    // Update is called once per frame
    void Update()
    {
        

        if(target == null) {return;}
        if (navMeshAgent == null) {return;}
        
        if (state == "Chasing")
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                
                _anim.SetTrigger("Attack");
                state = "Attacking";
                navMeshAgent.destination = transform.position;
                _anim.SetBool("is_running", false);

                //LookAt(target);
                //Vector3 lookPosition = target.position;
                //lookPosition.y = transform.position.y;
                //Vector3 lookDirection = (lookPosition - transform.position).normalized;
                //transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
        

            }
        }
        if (state == "Attacking")
        {
            LookAt(target);
        }
        if (state == "Chasing")
        {
            navMeshAgent.destination = target.position;
            _anim.SetBool("is_running", true);

        }
        

        //navMeshAgent.destination = target.position;
        //navMeshAgent.
    }

    public void SetTarget(Transform newTarget)
    {
        if (state == "Idle" || state == "Chasing")
        {
            target = newTarget;
            state = "Chasing";
            navMeshAgent.destination = target.position;
            _anim.SetBool("is_running", true);

        }
    }

    public void FollowTarget()
    {
        if (target == null)
        {
            state = "Idle";
        }
        else
        {
            state = "Chasing";
            navMeshAgent.destination = target.position;
            _anim.SetBool("is_running", true);

        }
    }
    private void LookAt(Transform pointOfInterest)
    {
        Vector3 lookPosition = pointOfInterest.position;
        lookPosition.y = transform.position.y;
        Vector3 lookDirection = (lookPosition - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
    }
}

