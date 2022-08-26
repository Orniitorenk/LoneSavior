using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuryingEnemy : MonoBehaviour
{
    EnemySpawningEnemies enemies;
    public float buryingMultiplier = 0.01f;

    private void Awake()
    {
        enemies = GetComponent<EnemySpawningEnemies>();
    }

    private IEnumerator Burying()
    {
        yield return new WaitForSeconds(0.1f);
        transform.Translate(Vector3.down * buryingMultiplier * Time.deltaTime , Space.Self);             
    }

    private void LateUpdate()
    {
        if (enemies.isDead)
        {
            StartCoroutine(Burying());
        }

        if(transform.position.y <= -0.6f)
        {
            Destroy(this.gameObject);
        }
    }
}
