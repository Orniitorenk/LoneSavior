using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 input;
    public float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float turnSpeed = 360;
    [SerializeField] Animator anim,anim1;
    public GameObject leftPallet, rightPallet;
    public bool isMoving;
    Vector3 lastPos;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
        CheckMovement();
    }


    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (input == Vector3.zero) return;
        

            var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        
        
        
    }

    private void Move()
    {
        rb.MovePosition(transform.position + (transform.forward * input.normalized.magnitude) * speed * Time.deltaTime);
        isMoving = true;
    }

    private void CheckMovement()
    {
         if (this.transform.position != lastPos)
         {
             anim.SetBool("isMoving", true);
             anim1.SetBool("isMoving", true);
             isMoving = true;
             if (isMoving)
             {
                 leftPallet.gameObject.SetActive(true);
                 rightPallet.gameObject.SetActive(true);
             }
         }
         else
         {
             anim.SetBool("isMoving", false);
             anim1.SetBool("isMoving", false);
             isMoving = false;
             leftPallet.gameObject.SetActive(false);
             rightPallet.gameObject.SetActive(false);
         }

         lastPos = this.transform.position;
         

        
    }
    
}
