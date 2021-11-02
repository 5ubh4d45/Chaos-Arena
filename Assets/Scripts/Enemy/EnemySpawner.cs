using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject[] enemyTypes;
    public float spwanDelay = 0.2f;
    public int noOfSpawns = 8;

    public static EnemySpawner instance;
    
    private void Awake() {
        instance = this;
        
    }
    public void SpawnEnemy(Vector3 spawnPos){
 
        //storing the diying position of the enemy
        Vector3 currentPosition = spawnPos;
        int randomEnemyNo = Random.Range(0, noOfSpawns);

        for (int i = 0; i < randomEnemyNo; i++ ){

            //choosing random enemy
            GameObject randomSpawn = enemyTypes[Random.Range(0, enemyTypes.Length)];
            
            
            //choosing random spawnpoint
            //Transform randomSpawnPoint = currentPosition * Random.Range(-1f, 1f);

            Instantiate(randomSpawn, currentPosition, Quaternion.identity);    
        }
    }
}
