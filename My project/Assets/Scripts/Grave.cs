using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    Object zombieRef;
    private bool selected;

    void Start()
    {
        selected = false;
        zombieRef = Resources.Load("Zombie");

    }

    void OnDisable()
    {

    }

    public void SpawnZombie()
    {
        GameObject Zombie = (GameObject)Instantiate(zombieRef, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
