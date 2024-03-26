using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody rb;

    Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(moveDir != Vector3.zero)
            rb.position += moveSpeed * moveDir * Time.fixedDeltaTime; 

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
            moveDir.z = 1;
        else if(Input.GetKey(KeyCode.S))
            moveDir.z = -1;
        else 
            moveDir.z = 0;
        
        if(Input.GetKey(KeyCode.A))
            moveDir.x = -1;
        else if(Input.GetKey(KeyCode.D))
            moveDir.x = 1;
        else 
            moveDir.x = 0;

        if(moveDir != Vector3.zero) //to avoid console prompt Look dir is zero 
            transform.forward = moveDir;
    }
}
