using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float lineOfSight = 8f;
    public float roamingRange = 4f;
    public float roamingSpeed = 1f;
    public float roamTime = 3f;
    private float nextRoamTime = 0f;

    [Header("Particle effect")]
    public GameObject hitEffect;
    public float effectDestroytime = 1f;

    [Header("CameraShake")]
    public float intensity = 1f;
    public float frequency = 3f;
    public float time = 0.2f;

    [Header("Combat")]
    public float shootingRange = 4f;
    public float fireRate = 1f;
    public float attackDamage = 5f;
    public float attackSpeed = 1f;
    private float canAttack = 0f;

    private float nextFireTime = 0f;
    public GameObject enemyBullet;
    public GameObject attackPoint;
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private Transform target;


    private void Start() {
        // getting target
        target = GameObject.FindGameObjectWithTag("Player").transform;

        //getting the starting position; and then setting first roam position
        startingPosition = transform.position;

        roamPosition = GetRoamingPosition();
    }
    private void FixedUpdate() {

        //calling enemy movement
        EnemyMovement();

        //Shooting condition
        EnemyShootCond();

    }

    
    private void EnemyMovement(){
        // move the enemy towards player if player in range
        float distanceFromTarget = Vector2.Distance(target.position, transform.position);

        if (distanceFromTarget < lineOfSight && distanceFromTarget > shootingRange){
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        
        // rotate the enemy towards player
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 enemyDirection = target.position - transform.position;
        float angle = Mathf.Atan2(enemyDirection.y , enemyDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        } 
        // roam if out of line of sight
        else if (distanceFromTarget > lineOfSight && distanceFromTarget > shootingRange) {
            // go to the roaming position
            GotoRoamingPos();
        }
        // if in shooting range stop and shoot
        else{
            // rotate the enemy towards player
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector3 enemyDirection = target.position - transform.position;
            float angle = Mathf.Atan2(enemyDirection.y , enemyDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
        
    }

    private Vector3 GetRoamingPosition(){
        // finding a random direction within roaming range
        float rndX = Random.Range(-1f, 1f);
        float rndY = Random.Range(-1f, 1f);
        // asssigning to a random direction
        Vector3 randomDir = new Vector3(rndX,rndY, 0f) * roamingRange;
        return transform.position + randomDir;
    }

    private void GotoRoamingPos(){
        // if not in range roam
        transform.position = Vector2.MoveTowards(this.transform.position, roamPosition, roamingSpeed * Time.deltaTime);

        // rotate the enemy towards target
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 enemyDirection = roamPosition - transform.position;
        float angle = Mathf.Atan2(enemyDirection.y , enemyDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;    

        //if near roam position or certain time passed, search new roam position
        float reachedPositionDistance = 1f;
                
        if(Vector2.Distance(transform.position, roamPosition) < reachedPositionDistance || nextRoamTime < Time.time ){
            
            //increase next roamtime
            nextRoamTime = Time.time + roamTime;
            // near roam position
            roamPosition = GetRoamingPosition();
        }
    }

    private void EnemyShootCond(){
         //chahecking distance from target to enemy
        float distanceFromTarget = Vector2.Distance(target.position, transform.position);

        if (distanceFromTarget <= shootingRange && nextFireTime < Time.time){
            EnemyShoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void EnemyShoot(){

        Instantiate(enemyBullet, attackPoint.transform.position, Quaternion.identity);
        SoundManager.instance.Play("laser2");
    }

    private void OnCollisionStay2D(Collision2D other) {
        
        // checks for player and calles the health to hurt player
        if (other.gameObject.tag == "Player"){
            if(attackSpeed <= canAttack){
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;

                //initiating the hit effect and the ndestroying the bullet an the effect with delay
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, effectDestroytime);
                
                CameraShake.instance.ShakeCamera(intensity, frequency, time);
            }
            else{
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    
    }



}

