using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable
{
    public float maxHealth =10;
    public float currentHealth;
    public float power = 1;
    public float speed = 6;
    public Healthbar hpBar;

    public GameObject dropPrefab;
    // Start is called before the first frame update
    void Start()
    {  
        currentHealth = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            currentHealth = 0;
            onDeath();
        }
        hpBar.SetHealth(currentHealth);
    }

    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;
        hpBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void onDeath()
    {
        gameObject.TryGetComponent<IKillable>(out IKillable character);
        if (character != null)
        {
            character.Kill();
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        GetComponent<IMovable>().SetSpeed(speed);
    }

    public void SetPower(float _newPower)
    {
        power = _newPower;
    }
}
