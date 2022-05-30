using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour, IMovable
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
        navMeshAgent.speed = GetComponent<CharacterStats>().speed;
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
            }
        }
        if (state == "Attacking")
        {
            LookAt(target);
            FollowTarget();
        }

        if (state == "Chasing")
        {
            navMeshAgent.destination = target.position;
            _anim.SetBool("is_running", true);
        }
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            _anim.SetBool("is_running", false);
        }
        

    }

    public void SetTarget(Transform newTarget)
    {
        if (state == "Idle" || state == "Chasing")
        {
            target = newTarget;
            FollowTarget();
            //state = "Chasing";
            //navMeshAgent.destination = target.position;
            //_anim.SetBool("is_running", true);
        }
    }

    public void FollowTarget()
    {
        if (target == null)
        {
            state = "Idle";
            //_anim.SetBool("is_running", false);

        }
        else
        {
            if (navMeshAgent.isOnNavMesh)
            {
                state = "Chasing";
                navMeshAgent.destination = target.position;
                _anim.SetBool("is_running", true);
            }
            else
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            

        }
    }
    private void LookAt(Transform pointOfInterest)
    {
        Vector3 lookPosition = pointOfInterest.position;
        lookPosition.y = transform.position.y;
        Vector3 lookDirection = (lookPosition - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
    }

    public void SetVelocity(Vector3 roamingPosition)
    {
        //randomPos = roamingPosition;
        //state = "Wandering";

    }
    public void SetSpeed(float _speed)
    {
        navMeshAgent.speed = _speed;

    }
}

