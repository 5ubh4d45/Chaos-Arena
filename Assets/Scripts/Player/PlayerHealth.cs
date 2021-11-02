using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{  
    public static PlayerHealth  instance;

    
    public Slider slider;

    public float healthReward = 20f;
    public GameObject healthGainEffect;
    public int healthGainScoreinterval = 1000;
    private int scorehGained = 0;
    

    [SerializeField] public float maxHealth = 100f;
    private float currentHealth = 0f;
    

    private void Awake() {
        instance = this;
    }
    private void Start()
    {
        currentHealth = maxHealth;

        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void UpdateHealth(float addHP){

        //takes in HP as argument and adds that to current health
        currentHealth += addHP;
        slider.value = currentHealth;

        // checks player health so it doen't exceed or drops below max or min health
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        } else if (currentHealth <= 0f){
            currentHealth = 0f;
            PlayerDied();

        }
    }

    private void PlayerDied(){
        PauseUI.instance.DieScreen();
        //Debug.Log("player died");
    }

    public void HealthGain(int addedscore){

        // setting the scoregain to current score
        scorehGained += addedscore;

        // logic for gained score to give health
        if (scorehGained >= healthGainScoreinterval){
            // giving health
            UpdateHealth(healthReward);

            // animation for health gain
            GameObject effect = Instantiate(healthGainEffect, transform.position, Quaternion.identity);

            //playing powerup sound
            SoundManager.instance.Play("powerup4");

            Destroy(effect, 1f);

            //restting the gained health
            scorehGained = 0;
        }
    }

}
