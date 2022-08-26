using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceToFly : MonoBehaviour
{
    private Rigidbody rb;
    private float forceModifier = 7;
    private bool isApplied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isApplied)
        {
            ForceApplied();
        }
        

    }

    void ForceApplied()
    {
        rb.AddForce(Vector3.up * forceModifier, ForceMode.Impulse);
        rb.AddForce(Vector3.right, ForceMode.Impulse);
        //rb.AddForce(Vector3.right, ForceMode.Impulse);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0,90,0) , 5);
        isApplied = true;
    }
}
