using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedForward = 50f;
    [SerializeField] private float speedBackward = 30f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float turnSpeedCanon = 10f;

    [SerializeField] private Transform canonTransform;

    private Rigidbody rigidbody;
    
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        CheckForInput();
        RotateCanonToCurser();
    }


    void CheckForInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");      

        if(vertical != 0f) {
            MovePlayer(vertical);
        }

        if(horizontal != 0f) {
            RotatePlayer(horizontal);
        }       
    }


    private void MovePlayer(float vertical)
    {
        Vector3 direction = new Vector3(0f, 0f, vertical) * Time.deltaTime;

        if (vertical > 0f)
        {
            rigidbody.AddRelativeForce(direction * speedForward);
        }

        if (vertical < 0f)
        {
            rigidbody.AddRelativeForce(direction * speedBackward);
        }
    }


    private void RotatePlayer(float horizontal)
    {
        Quaternion rotation = Quaternion.Euler(0f, horizontal * turnSpeed * Time.fixedDeltaTime, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * rotation );
    }
    

    private void RotateCanonToCurser()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            Vector3 target = hit.point;
            target.y = canonTransform.position.y;
            Vector3 currentPos = canonTransform.position;
            Vector3 difference = target - currentPos;
            
            canonTransform.rotation = Quaternion.RotateTowards(canonTransform.rotation, Quaternion.LookRotation(difference), turnSpeedCanon * Time.fixedDeltaTime);
        }
    }
}
