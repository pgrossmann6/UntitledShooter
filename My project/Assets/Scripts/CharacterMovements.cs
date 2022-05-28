using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour, IMovable, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private Animator _animator;

    public float ccSpeed;

    private Vector3 velocityVector;
    private CharacterController _cc;
    

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetVelocity(Vector3 velocityVector)
    {

        //this.velocityVector 
        
        this.velocityVector = velocityVector;

        //Debug.Log(velocityVector);
    }

    public void Move()
    {
        Vector3 VelVect3 = (_animator.GetBool("CanMove"))? velocityVector : Vector3.zero;

        _cc.Move(VelVect3 * (speed * Time.deltaTime));
        ccSpeed =  _cc.velocity.magnitude;
        _animator.SetFloat("Speed", _cc.velocity.magnitude);
    }

    public void Damage(float damage)
    {
        Debug.Log(damage);
    }
}
