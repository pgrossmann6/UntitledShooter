using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour,IKillable
{

    private NavMeshAI AI;
    Object grave;
    private Animator _anim;

    void Start()
    {
        grave = Resources.Load("Grave");

        AI = GetComponent<NavMeshAI>();
        StartCoroutine("FindTarget");
        

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (AI.target != null)
        {
            if (!AI.navMeshAgent.isStopped)
            {
                if (Vector3.Distance(transform.position, AI.target.transform.position) <= 2)
                {
                    //close distance
                    AI.navMeshAgent.SetDestination(transform.position);
                    //AI.target = null;
                    _anim.SetTrigger("Attack");
                }
            }
        }
        */
    }

    private IEnumerator FindTarget()
    {

        while(this.enabled)
        {
            AI.SetTarget(GameObject.FindGameObjectWithTag("Player").transform);

            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 20f);
            foreach (Collider collider in colliderArray)
            {
                //Debug.Log(collider.name);
                if (collider.tag =="Zombie" || collider.tag == "Player")
                {
                    if (AI.target == null)
                    {
                        AI.SetTarget(collider.transform);
                    }
                    else if (Vector3.Distance(collider.transform.position, transform.position) < Vector3.Distance(AI.target.position, transform.position))
                    {
                        AI.SetTarget(collider.transform);
                    }
                }

            }

            yield return new WaitForSeconds(1);
        }
        
        //FindTarget();

    }
    public void OnDestroy()
    {
    }

    public void Kill()
    {
        GameObject EnemyGrave = (GameObject)Instantiate(grave, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    public void Attack( IDamageable opponent)
    {
        opponent.Damage(GetComponent<CharacterStats>().power);
    }
}
