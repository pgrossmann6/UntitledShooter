using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    //PlayerInputHandler pih;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputHandler.casting += cast;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cast()
    {
        Debug.Log("CASTOU");
    }
}
