using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour, IMovable
{
    [SerializeField] private float speed;
    private Vector3 velocityVector;
    private CharacterController _cc;
    

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
        //Debug.Log(velocityVector);
    }

    public void Move()
    {
        _cc.Move(velocityVector * (speed * Time.deltaTime));
    }
}
