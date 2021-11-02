using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulllet_2 : MonoBehaviour
{
    // This is the Homing Enemy Bullet
    GameObject target;
    public float speed = 6f;
    public float damage = 2f;
    public float bulletDisappearTime = 5f;

    [Header("Particle effect")]
    public GameObject hitEffect;
    public float effectDestroytime = 1f;

    [Header("CameraShake")]
    public float intensity = 1f;
    public float frequency = 3f;
    public float time = 0.2f;

    Rigidbody2D bulleteRB;    
    void Start()
    {   
        // get the Target
        target = GameObject.FindGameObjectWithTag("Player");
        Destroy(this.gameObject, bulletDisappearTime);
    }
    private void FixedUpdate() {
        // track and follow the player
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //checks for player
            if(other.gameObject.tag == "Player" || other.gameObject.tag == "Environment"){

                if(other.gameObject.tag == "Player"){
                    //player damage fn
                    CameraShake.instance.ShakeCamera(intensity, frequency, time);

                    //hit sound
                    SoundManager.instance.Play("hithurt3");
                    
                   //cam shake
                   other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);
                   //Debug.Log("Enemy_3 hit player");
               }
            //initiating the hit effect and the ndestroying the bullet an the effect with delay
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectDestroytime);
            Destroy(this.gameObject);
            }
        }

}
