using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{   

    public float damage = 20f;
    public float effectDestroytime = 1f;
    public GameObject hitEffect;

    [Header("CameraShake")]
    public float intensity = 4f;
    public float frequency = 10f;
    public float time = 0.08f;
    

    //detecting the bullect collision and performing certain tasks
    private void OnCollisionEnter2D(Collision2D bulletCollision) {

        //detecting enemy
        if (bulletCollision.gameObject.tag == "Enemy"){
            //adding damage to enemy
            bulletCollision.gameObject.GetComponent<EnemyHealth>().UpdateHealth(-damage);
            //shake cam
            CameraShake.instance.ShakeCamera(intensity, frequency, time);
        }

        //initiating the hit effect and the ndestroying the bullet an the effect with delay
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, effectDestroytime);
        Destroy(gameObject);
    }

}
