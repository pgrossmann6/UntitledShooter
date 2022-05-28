using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject spellSpawnPosition;
    [SerializeField] private Animator _animator;
    private Grave _grave;

    public float magicDamage;

    Object spellRef;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();


        PlayerInputHandler.casting += CastSpell;
        PlayerInputHandler.resurrecting += Resurrecting;


        spellRef = Resources.Load("Spell");
    }

    void OnDisable()
    {
        PlayerInputHandler.casting -= CastSpell;
        PlayerInputHandler.resurrecting -= Resurrecting;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void CastSpell()
    {
        if(_animator.GetBool("CanCast") == false) {return;}

        _animator.SetTrigger("Cast");
        _animator.SetBool("CanCast", false);
        //GameObject spell = (GameObject)Instantiate(spellRef, spellSpawnPosition.transform.position, transform.rotation);
    }

    public void SpawnSpell()
    {
        //Debug.Log("FOOOII??????");
        GameObject spell = (GameObject)Instantiate(spellRef, spellSpawnPosition.transform.position, transform.rotation);
        spell.GetComponent<Projectile>().magic_power = magicDamage;
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
                //_grave = grave;
                //grave.SpawnZombie();
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
