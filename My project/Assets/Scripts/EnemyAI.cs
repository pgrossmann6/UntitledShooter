using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    private NavMeshAI AI;
    Object grave;
    void Start()
    {
        grave = Resources.Load("Grave");

        AI = GetComponent<NavMeshAI>();
        StartCoroutine("FindTarget");
        

    }

    // Update is called once per frame
    void Update()
    {

        if (AI.target != null)
        {
            //Vector3 lookPosition = AI.target.position;
            //lookPosition.y = transform.position.y;
            //Vector3 lookDirection = (lookPosition - transform.position).normalized;
            //transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
        }
    }

    private IEnumerator FindTarget()
    {
        AI.target = GameObject.FindGameObjectWithTag("Player").transform;

        while(this.enabled)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 20f);
            foreach (Collider collider in colliderArray)
            {
                //Debug.Log(collider.name);
                if (collider.tag =="Zombie" || collider.tag == "Player")
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
            Debug.Log("foi");

            yield return new WaitForSeconds(1);
        }
        
        FindTarget();

    }

    public void DealDamage(float d)
    {
        GameObject EnemyGrave = (GameObject)Instantiate(grave, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
