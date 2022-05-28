using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    Object zombieRef;

    void Start()
    {
        zombieRef = Resources.Load("Zombie");
    }

    public void SpawnZombie()
    {
        GameObject Zombie = (GameObject)Instantiate(zombieRef, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
