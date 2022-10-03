using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectOnPlaneWalls : MonoBehaviour
{
    public GameObject Plane;
    float moveSpeed = 50;
    public CapsuleCollider col;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 input = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //input.Normalize();
        input = transform.TransformDirection(input);
        input = Vector3.ProjectOnPlane(input, Plane.transform.up);
        
        Vector3 p1 = transform.position + col.center - transform.up * col.height * 0.5f;
        Vector3 p2 = p1 + transform.up * col.height;
        Vector3 move = input * moveSpeed * Time.deltaTime;
        RaycastHit hit;
        float distance = move.magnitude;
        if (Physics.CapsuleCast(p1, p2, col.radius, move, out hit, distance))
        {
            // make sure it's a wall in some way-  tag, collection, layermask ...
            if (hit.transform.tag == "Wall")
            {
                //we can follow the wall projection
                move = Vector3.ProjectOnPlane(move, hit.normal);
                // and also rotate to face our new projected movement.
                transform.rotation = Quaternion.LookRotation(move);
            }
        }

        rb.MovePosition(transform.position + move);
    }

}
