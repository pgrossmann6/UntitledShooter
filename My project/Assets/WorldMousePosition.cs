using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMousePosition : MonoBehaviour
{
    [SerializeField] public GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {return;}
        pointer.transform.position = hit.point;
    }
}
