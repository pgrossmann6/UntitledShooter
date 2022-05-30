using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject spellSpawnPosition;
    [SerializeField] private Animator _animator;
    public LayerMask layerGrave;
    private Grave _grave;
    [SerializeField] private int zombieCount;

    Object spellRef;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        spellRef = Resources.Load("Spell");

        PlayerInputHandler.casting += CastSpell;
        PlayerInputHandler.resurrecting += Resurrecting;
        ZombieAI.Spawned += AddZombieCount;
        ZombieAI.Died += RemoveZombieCount;
    }

    void OnDisable()
    {
        PlayerInputHandler.casting -= CastSpell;
        PlayerInputHandler.resurrecting -= Resurrecting;
        ZombieAI.Died-= RemoveZombieCount;
        ZombieAI.Spawned -= AddZombieCount;

    }

    private void CastSpell()
    {
        if(PauseMenu.GameIsPaused) {return;}
        if(_animator.GetBool("CanCast") == false) {return;}
        _animator.SetTrigger("Cast");
        _animator.SetBool("CanCast", false);
    }

    public void SpawnSpell()
    {
        GameObject spell = (GameObject)Instantiate(spellRef, spellSpawnPosition.transform.position, transform.rotation);
        spell.GetComponent<Projectile>().magic_power = GetComponent<CharacterStats>().power;
        spell.GetComponent<Projectile>().isPiercing = GetComponent<Wizard>().isPiercing;
        //spell.GetComponent<Projectile>().SetSpeed(GetComponent<Wizard>().SpellSpeed);

    }

    private void Resurrecting()
    {
        if(_animator.GetBool("CanCast") == false) {return;}
        if(zombieCount > 10) {return;}

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, layerGrave))
        {
            hit.transform.gameObject.TryGetComponent<Grave>(out _grave);
            if (_grave != null)
            {
                _animator.SetTrigger("Resurrection");
            }
        }
    }

    public void ResurrectGrave()
    {
        if (_grave != null)
        {
            _grave.SpawnZombie();
            _grave = null;
        }
    }
    public void RemoveZombieCount()
    {
        zombieCount--;
    }
    public void AddZombieCount()
    {
        zombieCount++;
    }
}
