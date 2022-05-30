using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieAI : MonoBehaviour, IKillable
{
    private NavMeshAI AI;
    public float strenght;
    private float witherSpeed;
    [SerializeField] private Transform randomPos;
    public static event Action Spawned;

    public static event Action Died;



    void Start()
    {
        AI = GetComponent<NavMeshAI>();
        StartCoroutine("FindTarget");
        witherSpeed = 0.5f *(Wizard.ZombieDecay);
        StartCoroutine("Wither");
        //StartCoroutine("WanderAround");
        Spawned?.Invoke();
        strenght = Wizard.ZombiePower;
        GetComponent<CharacterStats>().SetMaxHealth(Wizard.ZombieHealth);
        GetComponent<IMovable>().SetSpeed(Wizard.ZombieSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnDestroy()
    {
        //StopAllCoroutines();
    }

    private IEnumerator FindTarget()
    {
        while(true)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 20f);
            foreach (Collider collider in colliderArray)
            {
                if (collider.tag =="Enemy")
                {
                    //Debug.Log(collider.name);
                    
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
        Died?.Invoke();
    }
    private IEnumerator Wither()
    {
        yield return new WaitForSeconds(witherSpeed);
        GetComponent<CharacterStats>().Damage(1f);
        //Debug.Log("TOMOU");
        StartCoroutine("Wither");

    }

    private IEnumerator WanderAround()
    {
         yield return new WaitForSeconds(2);
        /*
       
        Vector3 roaming = new Vector3((UnityEngine.Random.Range(-1,1)*10),0, UnityEngine.Random.Range(-1,1)*10);
        Debug.Log(roaming+transform.position);
        randomPos.position += roaming;
        GetComponent<IMovable>().SetVelocity(randomPos.position);
        StartCoroutine("WanderAround");
        */

    }
}
