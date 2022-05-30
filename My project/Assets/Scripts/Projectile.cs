using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float destroyAfterSeconds = 5f;
    public float magic_power;
    [SerializeField] public float launchForce = 10f;
    [SerializeField] public bool isPiercing = false;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * Wizard.SpellSpeed;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * Wizard.SpellSize;
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDisable()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<IDamageable>(out IDamageable enemy);
        if (enemy != null && other.tag == "Enemy")
        {
            enemy.Damage(magic_power);
            if (!isPiercing) { Destroy(gameObject);}
        }
    }
/*
    public void SetSpeed(float _force)
    {
        Debug.Log(_force);
        rb.velocity = transform.forward * _force;

    }*/
}
