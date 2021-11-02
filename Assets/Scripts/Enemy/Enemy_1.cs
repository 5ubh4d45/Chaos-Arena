using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float roamingRange = 4f;
    public float roamingSpeed = 1f;
    public float roamTime = 3f;
    private float nextRoamTime = 0f;

    [Header("Combat")]
    public float lineOfSight = 8f;
    public float attackDamage = 5f;
    public float attackSpeed = 1f;
    private float canAttack = 0f;

    [Header("Particle effect")]
    public GameObject hitEffect;
    public float effectDestroytime = 1f;

    [Header("CameraShake")]
    public float intensity = 1f;
    public float frequency = 3f;
    public float time = 0.2f;

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
        
    }

    
    private void EnemyMovement(){
        // move the enemy towards player if player in range
        float distanceFromTarget = Vector2.Distance(target.position, transform.position);

        if (distanceFromTarget < lineOfSight){
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        
            // rotate the enemy towards player
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector3 enemyDirection = target.position - transform.position;
            float angle = Mathf.Atan2(enemyDirection.y , enemyDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        } else {
            // go to the roaming position
            GotoRoamingPos();

        }
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

                //hit sound
                SoundManager.instance.Play("hithurt3");
                
                CameraShake.instance.ShakeCamera(intensity, frequency, time);
            }
            else{
                canAttack += Time.deltaTime;
            }
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
    

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }



}

