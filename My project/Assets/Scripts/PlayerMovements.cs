using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _inputs;
    [SerializeField] private bool is_playable;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GameObject.FindObjectOfType<PlayerInputHandler>();
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_playable) return;
        Vector3 moveVector = new Vector3 (_inputs.move.x, -10, _inputs.move.y);
        GetComponent<IMovable>().SetVelocity(moveVector);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        LayerMask layer = 1 << 6;
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer)) {return;}

        Vector3 lookPosition = hit.point;
        lookPosition.y = transform.position.y;
        Vector3 lookDirection = (lookPosition - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
        
    }

}
