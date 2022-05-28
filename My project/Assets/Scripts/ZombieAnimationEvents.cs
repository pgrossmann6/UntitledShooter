using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationEvents : MonoBehaviour
{
    [SerializeField] private Transform attackPos;
    [SerializeField] private GameObject Zombie;

    public void DealDamage()
    {
        Collider[] colliderArray = Physics.OverlapSphere(attackPos.position, 2f);
        foreach (Collider collider in colliderArray)
        {
            collider.TryGetComponent<IDamageable>(out IDamageable opponent);
            if(opponent != null && collider.tag == "Enemy")
            {
                Zombie.GetComponent<ZombieAI>().Attack(opponent);
            }

        }
    }
}
