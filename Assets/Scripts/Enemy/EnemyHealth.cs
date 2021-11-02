using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float currentHealth = 0f;
    [SerializeField] private float maxHealth = 100f;

    public bool canSpawn = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float addHP){

        //takes in HP as argument and adds that to current health
        currentHealth += addHP;

        // checks player health so it doen't exceed or drops below max or min health
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        } else if (currentHealth <= 0f){
            currentHealth = 0f;
            EnemyDied();

        }
    }

    private void EnemyDied(){
        //Debug.Log("Enemy Died!");

        //adding score by calling the scoremanager instance
        ScoreManager.instance.AddScore();

        //deathsound
        SoundManager.instance.Play("explosion1");

        // calls the enemyspawner and gives the spawn location
        if (canSpawn){
            EnemySpawner.instance.SpawnEnemy(transform.position);
        }
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.1f);
    }

}
