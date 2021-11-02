using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{   
    [Header("ShootSettings")]
    public Transform firepoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float attackSpeed = 0.3f;
    private float nextAttack = 0f;

    [Header("CameraShake")]
    public float intensity = 1f;
    public float frequency = 3f;
    public float time = 0.2f;

    void Update()
    {
        ShootCheck();
    }

    //Checks if Can Enable Shoot or not
    private void ShootCheck(){
        //checks if mouse pressed or not
        if (Input.GetKey(KeyCode.Mouse0) && nextAttack <= Time.time){
            Shoot();
            nextAttack = attackSpeed + Time.time;
        }
    }

    //Initiates shoot
    private void Shoot(){
        //Get the bullet prefab and its Rigidbody to add force/velocity
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
        SoundManager.instance.Play("laser5");
        CameraShake.instance.ShakeCamera(intensity, frequency, time);
    }

}
