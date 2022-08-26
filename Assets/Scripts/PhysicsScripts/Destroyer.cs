using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        if(rb != null)
        {
            rb.GetComponent<Rigidbody>();
        }
        
    }
    private void Update()
    {
        //Destroy(this.gameObject, 4);
        StartCoroutine(DisableRB());
    }

    IEnumerator DisableRB()
    {
        yield return new WaitForSeconds(2);
        rb.useGravity = false;
        rb.detectCollisions = false;      
    }
}
