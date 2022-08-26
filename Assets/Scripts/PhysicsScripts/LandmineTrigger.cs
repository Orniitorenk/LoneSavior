using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineTrigger : MonoBehaviour
{
    public float damage = 25;
    public GameObject landmineExplosion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lav25Spawned"))
        {
            GameObject enemyPrefab = other.transform.gameObject;
            enemyPrefab.GetComponent<EnemySpawningEnemies>().TakeDamage(damage);
            Instantiate(landmineExplosion, transform.position, Quaternion.Euler(-90f, 0, 0f));
            Destroy(gameObject, 0.5f);
        }
    }

}
