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
        PlayerInputHandler.resurrecting += SpawnZombie;

    }

    void OnDisable()
    {
        PlayerInputHandler.resurrecting -= SpawnZombie;

    }

    private void SpawnZombie()
    {
        if (selected == true)
        {
            GameObject Zombie = (GameObject)Instantiate(zombieRef, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
    private void OnMouseEnter()
    {
        selected = true;
    }
    private void OnMouseExit()
    {
        selected = false;
    }
}
