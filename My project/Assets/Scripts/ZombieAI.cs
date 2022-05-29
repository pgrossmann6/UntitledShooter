using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour, IKillable
{
    private NavMeshAI AI;
    public float strenght;
    void Start()
    {
        AI = GetComponent<NavMeshAI>();
        StartCoroutine("FindTarget");
        StartCoroutine("Wither");

    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator FindTarget()
    {
        while(true)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 20f);
            foreach (Collider collider in colliderArray)
            {
                //Debug.Log(collider.name);
                if (collider.tag =="Enemy")
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
        

    }

    public void Attack( IDamageable opponent)
    {
        opponent.Damage(strenght);
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    private IEnumerator Wither()
    {
        yield return new WaitForSeconds(2);
        GetComponent<CharacterStats>().Damage(1f);
        Debug.Log("TOMOU");
        StartCoroutine("Wither");

    }
}
