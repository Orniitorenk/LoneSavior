using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] enemySpawnPoints;
    public Transform[] healthSpawnPoints;

    [Header("Wave Values")]
    public GameObject healthPoints;
    public Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;


    [Header("Booleans")]
    private bool canSpawn = true;
      
    [Header("Game Manager")]
    public GameManager GM;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Lav25Spawned");

        if (totalEnemies.Length <= 0 && !canSpawn && currentWaveNumber+1 != waves.Length)
        {          
            // Spawn enemies certain vawe numbers
            if(currentWaveNumber == 0 && !UI_Manager.Instance.opened)
            {
                UI_Manager.Instance.OpenUpgradePanel();
                SpawnHealthGO(0, 1);
                GM.upgradeState = true;
            }

            if(currentWaveNumber == 4 && !UI_Manager.Instance.opened)
            {
                UI_Manager.Instance.OpenUpgradePanel();
                SpawnHealthGO(2, 3);
                GM.upgradeState = true;
            }
            if(currentWaveNumber == 8 && !UI_Manager.Instance.opened)
            {
                UI_Manager.Instance.OpenUpgradePanel();
                SpawnHealthGO(4, 5);
                GM.upgradeState = true;
            }
            if(currentWaveNumber == 12 && !UI_Manager.Instance.opened)
            {
                UI_Manager.Instance.OpenUpgradePanel();
                SpawnHealthGO(6, 7);
                GM.upgradeState = true;
            }
            if(currentWaveNumber == 16 && !UI_Manager.Instance.opened)
            {
                UI_Manager.Instance.OpenUpgradePanel();
                SpawnHealthGO(8, 9);
                GM.upgradeState = true;
            }
            if (currentWaveNumber == 20 && !UI_Manager.Instance.opened)
            {
                UI_Manager.Instance.OpenUpgradePanel();
                SpawnHealthGO(10, 11);
                GM.upgradeState = true;
            }

            if (!UI_Manager.Instance.opened)
            {
                SpawnNextWave();
            }
        }
        else if(currentWaveNumber+1 == waves.Length)
        {
            Debug.Log("ended");
            UI_Manager.Instance.gameEnded = true;
        }
    }

    public void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {

            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
                UI_Manager.Instance.opened = false;
            }           
        }       
    }

    public void SpawnNextWave() //button listener
    {
        currentWaveNumber++;
        canSpawn = true;
    }   

    public void SpawnHealthGO(int index, int index1)
    {
        Instantiate(healthPoints, healthSpawnPoints[index].position + new Vector3(0f, 0.5f, 0), Quaternion.identity);
        Instantiate(healthPoints, healthSpawnPoints[index1].position + new Vector3(0f, 0.5f, 0), Quaternion.identity);
    }
}
