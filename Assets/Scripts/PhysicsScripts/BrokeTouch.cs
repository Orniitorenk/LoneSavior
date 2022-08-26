using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeTouch : MonoBehaviour
{
    public GameObject brokenStat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(brokenStat, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Lav25Spawned"))
        {
            Instantiate(brokenStat, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
