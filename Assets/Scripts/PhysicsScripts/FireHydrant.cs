using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour
{
    public GameObject waterSparkles;
    private bool spawned;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            Instantiate(waterSparkles, transform.position, Quaternion.Euler(-90f,0f,0f));
            spawned = true;
        }
    }

    private void Update()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Sparkles");

        if (spawned)
        {                       
            Destroy(obj.gameObject, 5f);            
            Destroy(this.gameObject, 5f);
        }
        
    }
}
