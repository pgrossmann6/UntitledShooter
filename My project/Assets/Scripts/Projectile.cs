using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float destroyAfterSeconds = 5f;
    [SerializeField] private float launchForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * launchForce;
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<IDamageable>(out IDamageable enemy);
        if (enemy != null)
        {
            enemy.DealDamage(1f);
            Destroy(gameObject);
        }
    }
}
