using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject spellSpawnPosition;
    Object spellRef;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputHandler.casting += CastSpell;

        spellRef = Resources.Load("Spell");
    }

    void OnDisable()
    {
        PlayerInputHandler.casting -= CastSpell;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void CastSpell()
    {
        
        GameObject spell = (GameObject)Instantiate(spellRef, spellSpawnPosition.transform.position, transform.rotation);
    }
}
