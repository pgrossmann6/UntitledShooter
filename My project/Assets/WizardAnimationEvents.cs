using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimationEvents : MonoBehaviour
{
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CastSpell()
    {
        //Debug.Log("FUNCIONOU");
        Player.GetComponent<PlayerCast>().SpawnSpell();
    }

    public void ResurrectGrave()
    {
        Player.GetComponent<PlayerCast>().ResurrectGrave();
    }
}
