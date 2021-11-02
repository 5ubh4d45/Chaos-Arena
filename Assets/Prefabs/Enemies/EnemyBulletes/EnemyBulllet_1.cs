using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulllet_1 : MonoBehaviour
{
    // This is classic Enemy Bullet
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
        // whenever bullet spawns, it finds player then launce itself.
        bulleteRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulleteRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, bulletDisappearTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //checks for player
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Environment"){
               
               if(other.gameObject.tag == "Player"){
                    //player damage fn
                    other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);

                    //hit sound
                    SoundManager.instance.Play("hithurt3");

                   //cam shake
                   CameraShake.instance.ShakeCamera(intensity, frequency, time);
                   //Debug.Log("Enemy_2 hit player");
               }
            //initiating the hit effect and the ndestroying the bullet an the effect with delay
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectDestroytime);
            Destroy(this.gameObject);
        }
        }

}
