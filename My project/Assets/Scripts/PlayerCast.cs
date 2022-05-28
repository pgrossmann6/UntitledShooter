using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject spellSpawnPosition;
    [SerializeField] private Animator _animator;
    private Grave _grave;

    Object spellRef;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        spellRef = Resources.Load("Spell");

        PlayerInputHandler.casting += CastSpell;
        PlayerInputHandler.resurrecting += Resurrecting;
    }

    void OnDisable()
    {
        PlayerInputHandler.casting -= CastSpell;
        PlayerInputHandler.resurrecting -= Resurrecting;
    }

    private void CastSpell()
    {
        if(_animator.GetBool("CanCast") == false) {return;}
        _animator.SetTrigger("Cast");
        _animator.SetBool("CanCast", false);
    }

    public void SpawnSpell()
    {
        GameObject spell = (GameObject)Instantiate(spellRef, spellSpawnPosition.transform.position, transform.rotation);
        spell.GetComponent<Projectile>().magic_power = GetComponent<CharacterStats>().power;
    }

    private void Resurrecting()
    {
        if(_animator.GetBool("CanCast") == false) {return;}

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.name);
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
}
