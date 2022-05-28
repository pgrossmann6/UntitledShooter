using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimarionEvents : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject Knight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        Collider[] colliderArray = Physics.OverlapSphere(weapon.position, 2f);
        foreach (Collider collider in colliderArray)
        {
            collider.TryGetComponent<IDamageable>(out IDamageable opponent);
            if(opponent != null && collider.tag != "Enemy")
            {
                Knight.GetComponent<EnemyAI>().Attack(opponent);
            }

        }
    }
}
